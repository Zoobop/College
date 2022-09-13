using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EngineAPI
{
    internal abstract class EngineBase
    {
        private readonly int _versionMajor = 0;
        private readonly int _versionMinor = 1;
        private Thread _gameThread;

        private static Hashtable _objectRegistry = new Hashtable();
        private static List<Shape2D> _tileRegistry;

        protected Display Display { get; private set; }

        private static Action _startCallback;
        private static Action _updateCallback;
        private static Action _destroyCallback;

        public EngineBase(string title, int screenWidth, int screenHeight)
        {
            Display = new Display(title, screenWidth, screenHeight);
            _tileRegistry = new List<Shape2D>();

            Console.Title = $"ENGINE-DEBUGGER <{title}>";

            _gameThread = new Thread(GameLoop);
            _gameThread.Start();

            Application.Run(Display);
        }

        private void GameLoop()
        {
            DebugInitialize();

            Initialize();
            _startCallback();
            while (!Display.IsDisposed)
            {
                try
                {
                    Draw();
                    Display.BeginInvoke((MethodInvoker)delegate { Display.Refresh(); });
                    Tick();
                    _updateCallback();
                    Thread.Sleep(1);
                }
                catch { }
            }
            _destroyCallback();
            Terminate();
        }

        private void DebugInitialize()
        {
            // Display debug information
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"|ENGINE-DEBUGGER v(");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{_versionMajor}.{_versionMinor:D2}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") <");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Display.Title);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(">|\n");

            string line = "============================";
            for (int i = 0; i < Display.Title.Length; i++) line += '=';
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        #region Registration

        public static void RegisterObject(GameObject gameObject)
        {
            _startCallback += gameObject.Start;
            _updateCallback += gameObject.Update;
            _destroyCallback += gameObject.Destroy;

            _objectRegistry.Add(gameObject.GUID, gameObject);
            Debugger.Log($"{gameObject.LogView()} Created and Registered!", LogLevel.INFO);
        }

        public static void UnregisterObject(GameObject gameObject)
        {
            _startCallback -= gameObject.Start;
            _updateCallback -= gameObject.Update;
            _destroyCallback -= gameObject.Destroy;

            _objectRegistry.Remove(gameObject.GUID);
            Debugger.Log($"{gameObject.LogView()} Destroyed and Unregistered!", LogLevel.INFO);
        }

        public static Hashtable GetRegistry() => _objectRegistry;

        public static void RegisterTile(Shape2D shape)
        {
            _tileRegistry.Add(shape);
        }

        #endregion

        #region Override Methods

        protected abstract void Initialize();
        protected abstract void Draw();
        protected abstract void Tick();
        protected abstract void Terminate();

        #endregion
    }
}
