using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineAPI
{
    internal class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public static Vector2 Identity { get => new Vector2(1, 1); }
        public static Vector2 Zero { get => new Vector2(0, 0); }
        public static Vector2 Up { get => new Vector2(0, 1); }
        public static Vector2 Down { get => new Vector2(0, -1); }
        public static Vector2 Left { get => new Vector2(-1, 0); }
        public static Vector2 Right { get => new Vector2(1, 0); }


        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(float scalar)
        {
            X = scalar;
            Y = scalar;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Vector2(float scalar) => new Vector2(scalar);

        public override string ToString() => $"({X},{Y})";
    }

}
