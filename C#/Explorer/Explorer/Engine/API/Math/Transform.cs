using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineAPI
{
    internal class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public Transform()
        {
            Position = new Vector2();
            Scale = new Vector2();
        }

        public Transform(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }

        public void Move(float horizontal, float vertical)
        {
            Position.X += horizontal;
            Position.Y += vertical;
        }
    }
}
