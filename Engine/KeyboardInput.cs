using System;
using System.Windows.Forms;

namespace TheGame.Engine
{
    public enum KeyCodes
    {
        ShiftKey,
        ControlKey,
        Menu
    }

    class KeyboardInput
    {
        private enum KeyStates
        {
            Up,
            Down
        }

        static private KeyStates[] _states = new KeyStates[Enum.GetValues(typeof(CursorType)).Length];
        static private KeyCodes _down;
        static private KeyCodes _up;
        private string _downKey = String.Empty;
        private string _upKey = String.Empty;

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            _downKey = e.KeyCode.ToString();
            try
            {
                _down = (KeyCodes)Enum.Parse(typeof(KeyCodes), e.KeyCode.ToString());
                _states[(int)_down] = KeyStates.Down;
            }
            catch (Exception exception)
            {
                if (!(exception is ArgumentException))
                    throw exception;
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            _upKey = e.KeyCode.ToString();
            try
            {
                _up = (KeyCodes)Enum.Parse(typeof(KeyCodes), e.KeyCode.ToString());
                _states[(int)_up] = KeyStates.Up;
            }
            catch (Exception exception)
            {
                if (!(exception is ArgumentException))
                    throw exception;
            }
        }

        public void ResetInput()
        {
            _downKey = _upKey = String.Empty;
        }

        public string DownKey
        {
            get { return _downKey; }
        }

        public string UpKey
        {
            get { return _upKey; }
        }

        static public bool IsKeyDown(KeyCodes mouseButton)
        {
            return _states[(int)mouseButton] == KeyStates.Down;
        }
    }
}
