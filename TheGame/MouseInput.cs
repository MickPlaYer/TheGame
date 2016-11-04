using System;
using System.Windows.Forms;

namespace TheGame
{
    public enum MouseButtons
    {
        None,
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }

    class MouseInput
    {
        private enum MouseButtonStates
        {
            Up,
            Down
        }

        static private MouseButtonStates[] _states = new MouseButtonStates[Enum.GetValues(typeof(MouseButtons)).Length];
        static private MouseButtons _click;
        static private MouseButtons _down;
        static private MouseButtons _up;

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            _click = (MouseButtons)Enum.Parse(typeof(MouseButtons), e.Button.ToString());
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            _down = (MouseButtons)Enum.Parse(typeof(MouseButtons), e.Button.ToString());
            _states[(int)_down] = MouseButtonStates.Down;
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            _up = (MouseButtons)Enum.Parse(typeof(MouseButtons), e.Button.ToString());
            _states[(int)_up] = MouseButtonStates.Up;
        }

        public void ResetInput()
        {
            _click = _down = _up = MouseButtons.None;
        }

        public MouseButtons MouseClickButton
        {
            get { return _click; }
        }

        public MouseButtons MouseDownButton
        {
            get { return _down; }
        }

        public MouseButtons MouseUpButton
        {
            get { return _up; }
        }

        static public bool IsMouseDown(MouseButtons mouseButton)
        {
            return _states[(int)mouseButton] == MouseButtonStates.Down;
        }

        static public bool IsMouseDown()
        {
            foreach (var state in _states)
            {
                if (state == MouseButtonStates.Down)
                    return true;
            }
            return false;
        }
    }
}
