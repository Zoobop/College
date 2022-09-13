using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EngineAPI
{
    internal class Shape2D
    {
        public Transform Transform { get; private set; }
        public Bitmap Sprite { get; private set; }

        public Shape2D(string fileName, Transform transform)
        {
            Transform = transform;
            Image image = Image.FromFile(fileName);

            Sprite = new Bitmap(image, (int)Transform.Scale.X, (int)Transform.Scale.Y);
            EngineBase.RegisterTile(this);
        }

        public Shape2D(string fileName, Vector2 position, Vector2 scale)
        {
            Transform = new Transform(position, scale);
            Image image = Image.FromFile(fileName);

            Sprite = new Bitmap(image, (int)Transform.Scale.X, (int)Transform.Scale.Y);
            EngineBase.RegisterTile(this);
        }
    }
}
