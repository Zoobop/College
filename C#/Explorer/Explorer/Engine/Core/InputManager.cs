using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineAPI
{
    internal static class InputManager
    {
        private static GameObject _controlledObj;

        public static void SetUser(GameObject gameObject)
        {
            _controlledObj = gameObject;
            Display.Instance.KeyDown += gameObject.OnKeyDown;
            Display.Instance.KeyUp += gameObject.OnKeyUp;
        }
    }
}
