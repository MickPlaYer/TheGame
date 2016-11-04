using System.Drawing;

namespace TheGame
{
    interface IGameState
    {
        void Start();
        void Move();
        void Show(Graphics graphics);
    }
}
