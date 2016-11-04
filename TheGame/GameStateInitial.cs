using System.Drawing;

namespace TheGame
{
    class GameStateInitial : GameState
    {
        private int _count;
        private string _mouseTest = "Mouse Test";
        private string _keyUpTest = "Key Up Test";
        private string _keyDownTest = "Key Down Test";

        // 建構式
        public GameStateInitial(Game game)
            : base(game)
        {
            System.Threading.Thread.Sleep(500);
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
            Game.Graphics.DrawString(_count.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 10);
            Game.Graphics.DrawString("GameStateInitial", SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 25);
            Game.Graphics.DrawString(_mouseTest, SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 40);
            Game.Graphics.DrawString(_keyUpTest, SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 55);
            Game.Graphics.DrawString(_keyDownTest, SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 70);
            Font font = new Font("myfont", 50);
            Game.Graphics.DrawString("Press Space to Start", font, new SolidBrush(Color.White), 300, 300);
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
