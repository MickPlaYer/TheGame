using System.Drawing;

namespace TheGame.Engine
{
    class GameStateInitial : GameState
    {
        private int _count;
        private string _mouseTest = "Mouse Test";
        private string _keyUpTest = "Key Up Test";
        private string _keyDownTest = "Key Down Test";
        private SolidBrush _whiteBrush = new SolidBrush(Color.White);
        private Font _font = new Font("MS Sans Serif", 50);

        // 建構式
        public GameStateInitial(Game game)
            : base(game)
        {
            _game.ChangeLoadingProgress(33);
        }

        // 每次開始重整這個遊戲階段
        public override void Init()
        {
            _count = 0;
        }

        public override void Update()
        {
            _count++;
        }

        public override void Draw()
        {
            Game.Graphics.DrawString(_count.ToString(), SystemFonts.DefaultFont, _whiteBrush, 10, 10);
            Game.Graphics.DrawString("GameStateInitial", SystemFonts.DefaultFont, _whiteBrush, 10, 25);
            Game.Graphics.DrawString(_mouseTest, SystemFonts.DefaultFont, _whiteBrush, 10, 40);
            Game.Graphics.DrawString(_keyUpTest, SystemFonts.DefaultFont, _whiteBrush, 10, 55);
            Game.Graphics.DrawString(_keyDownTest, SystemFonts.DefaultFont, _whiteBrush, 10, 70);
            Game.Graphics.DrawString("Press Space to Start", _font, _whiteBrush, 100, 300);
        }

        public override void OnKeyUp(string key)
        {
            _keyUpTest = key;
            if (key == "Space")
                _game.GoToState(1);
        }

        public override void OnKeyDown(string key)
        {
            _keyDownTest = key;
        }

        public override void OnMouseClicked(MouseButtons mouseButton, int positionX, int positionY)
        {
            _mouseTest = mouseButton.ToString();
        }
    }
}
