using TheGame.Engine;

namespace TheGame.RTS
{
    abstract class Command
    {
        const int TEXT_SIZE = 20;
        const int ANCHOR = 40;
        private MovableBitmap _buttonBitmap;
        private MovableText _buttonText;
        private string _key;

        public Command(MovableBitmap buttonBitmap, string key)
        {
            _buttonBitmap = buttonBitmap;
            _key = key;
            _buttonText = new MovableText(key, TEXT_SIZE);
        }

        public abstract Action Cast(Unit castUnit);

        public void DrawButton(int top, int left)
        {
            _buttonBitmap.SetPosition(top, left);
            _buttonBitmap.Draw();
            _buttonText.SetPosition(top + ANCHOR, left + ANCHOR);
            _buttonText.Draw();
        }

        public string Key { get { return _key; } }
    }
}
