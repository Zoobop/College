using System;
using System.Drawing;
using System.Runtime.InteropServices;

using EngineAPI;

namespace Game
{
    internal class Launcher : EngineBase
    {
        char[,] _map =
        {
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '.', '.', '.', '.', '.', '.', '.' },
        };

        public Launcher(string title, int screenWidth = 1200, int screenHeight = 800) : base(title, screenWidth, screenHeight) { }

        static int i = 0;
        static int[] rgb = { 0, 0, 0 };

        Player player1;
        Player player2;

        protected override void Initialize()
        {
            player1 = new Player("Player1", new Vector2(-300, 0), Vector2.Identity, Color.Red);
            player2 = new Player("Player2", new Vector2(300, 0), Vector2.Identity, Color.Blue);

            InputManager.SetUser(player2);
        }

        protected override void Draw()
        {

        }

        protected override void Tick()
        {
            
        }

        protected override void Terminate()
        {
            
        }
    }

    internal class EntryPoint
    {
        static void Main(string[] args)
        {
            new Launcher("Dungeon Explorer", 1200, 800);
        }
    }
}
