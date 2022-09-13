using System;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineAPI
{
    internal class Display : Form
    {
        public static Display Instance { get; private set; }

        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        public string Title { get; private set; }
        public Color BackgroundColor { get; set; } = Color.Black;
        public static float ViewScale { get; private set; } = 20.0f;

        private SolidBrush _brush;

        public Display(string title, int screenWidth, int screenHeight) : base()
        {
            Instance = this;
            DoubleBuffered = true;

            Title = title;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            Text = title;
            Size = new Size(screenWidth, screenHeight);
            Paint += Render;

            _brush = new SolidBrush(Color.Red);
        }

        private void Render(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.Clear(BackgroundColor);

            foreach (DictionaryEntry obj in EngineBase.GetRegistry())
            {
                var gameObject = (GameObject)obj.Value;
                _brush.Color = gameObject.Color;
                float xScale = gameObject.Transform.Scale.X * ViewScale;
                float yScale = gameObject.Transform.Scale.Y * ViewScale;
                float xPos = (ScreenWidth / 2) + gameObject.Transform.Position.X - (xScale / 2);
                float yPos = (ScreenHeight / 2) - gameObject.Transform.Position.Y - (yScale / 2);
                //Debugger.Log($"Position: ({xPos},{yPos})");

                graphics.FillRectangle(_brush, xPos, yPos, xScale, yScale);
            }


        }
    }
}
