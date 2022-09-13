using TKCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineAPI
{
    public struct Timestep
    {
        private FrameEventArgs _args;

        public double Time => _args.Time;

        public Timestep(double elaspedTime)
        {
            _args = new FrameEventArgs(elaspedTime);
        }

        public static implicit operator FrameEventArgs(Timestep ts) => new FrameEventArgs(ts.Time);
        public static implicit operator Timestep(FrameEventArgs args) => new Timestep(args.Time);
    }

    public abstract class EngineBase : IDisposable
    {
        private readonly int _versionMajor = 0;
        private readonly int _versionMinor = 1;

        private static Action _startCallback = delegate { };
        private static Action<Timestep> _updateCallback = delegate { };
        private static Action _destroyCallback = delegate { };

        private static Hashtable _objectRegistry = new Hashtable();

        protected static Viewport? Viewport { get; private set; }
        protected static Camera? Camera { get; private set; }

        public EngineBase(string title, int screenWidth, int screenHeight)
        {
            Console.Title = $"ENGINE-DEBUGGER <{title}>";
            Camera = new Camera();
            Viewport = new Viewport(new ViewportSettings(title, screenWidth, screenHeight));

            Viewport.CenterWindow();

            Viewport.Load += OnLoad;
            Viewport.KeyDown += OnKeyDown;
            Viewport.MouseMove += OnMouseMove;
            Viewport.Resize += OnWindowResize;
            Viewport.RenderFrame += Render;
            Viewport.UpdateFrame += Update;
            Viewport.Closing += OnClose;

            DebugInitialize();
        }

        public void Run() => Viewport?.Run();

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
            Console.Write(Viewport?.Title);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(">|\n");

            string line = "============================";
            for (int i = 0; i < Viewport?.Title.Length; i++) line += '=';
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

        #endregion

        #region Event Methods
        private void OnLoad()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.2f, 0.5f, 0.8f, 0);
        }

        private void OnKeyDown(KeyboardKeyEventArgs args)
        {

        }

        private void OnMouseMove(MouseMoveEventArgs args)
        {
            if (Viewport!.IsMouseButtonDown(MouseButton.Middle))
            {
                //_camera!.AccAngle(args.DeltaY / 100.0f, args.DeltaX / 100.0f, 0.0f);
            }
        }

        private void OnWindowResize(ResizeEventArgs args)
        {
            GL.Viewport(0, 0, args.Width, args.Height);
        }

        private void Render(FrameEventArgs args) => OnRender(args);
        private void Update(FrameEventArgs args) => OnUpdate(args);

        private void OnClose(CancelEventArgs args)
        {
            
        }

        protected virtual void OnInitialize() => _startCallback?.Invoke();
        protected virtual void OnRender(Timestep ts) { }
        protected virtual void OnUpdate(Timestep ts) => _updateCallback?.Invoke(ts);
        protected virtual void OnTerminate() => _destroyCallback?.Invoke();

        #endregion

        #region IDisposable

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Viewport?.Dispose();
            OnTerminate();
        }

        #endregion
    }
}
