using System.Drawing;

namespace TheGame.Engine
{
    interface IGameState
    {
        void Start();
        void Move();
        void Show(Graphics graphics);
    }
}
