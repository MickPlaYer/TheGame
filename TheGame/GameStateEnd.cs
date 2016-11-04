using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheGame
{
    class GameStateEnd : GameState
    {
        private int _count;

        // 建構式
        public GameStateEnd(Game game)
            : base(game)
        {
            System.Threading.Thread.Sleep(500);
            _game.ChangeLoadingProgress(100);
        }

        // 每次開始重整這個遊戲階段
        public override void Init()
        {
            _count = 0;
        }

        public override void Update()
        {
            _count++;
            if (_count >= 100)
                _game.GoToState(0);
        }

        public override void Draw()
        {
            Font font = new Font("myfont", 50);
            Game.Graphics.DrawString(_count.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 10);
            Game.Graphics.DrawString("GameStateEnd", SystemFonts.DefaultFont, new SolidBrush(Color.White), 10, 25);
            Game.Graphics.DrawString("Game Over", font, new SolidBrush(Color.White), 500, 300);
        }
    }
}
