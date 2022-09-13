using EngineAPI;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    internal class Player : GameObject
    {
        public bool[] MovementDirections { get; set; } = new bool[] { false, false, false, false };
        public float MovementSpeed { get; set; } = 3.0f;

        public Player(string name, Transform transform, Color color) : base(name, transform)
        {
            Name = name;
            Color = color;
        }

        public Player(string name, Vector2 position, Vector2 scale, Color color) : base(name, position, scale)
        {
            Name = name;
            Color = color;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)        MovementDirections[0] = true;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)      MovementDirections[1] = true;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)      MovementDirections[2] = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)     MovementDirections[3] = true;
        }

        public override void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)    MovementDirections[0] = false;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)  MovementDirections[1] = false;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)  MovementDirections[2] = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) MovementDirections[3] = false;
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            if (MovementDirections[0])  Transform.Move(0, MovementSpeed);
            if (MovementDirections[1])  Transform.Move(-MovementSpeed, 0);
            if (MovementDirections[2])  Transform.Move(0, -MovementSpeed);
            if (MovementDirections[3])  Transform.Move(MovementSpeed, 0);
        }

        public override void Destroy()
        {
            
        }
    }
}
