using System.Drawing;
using TheGame.Engine;

namespace TheGame
{
    enum CameraMotion
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    class Camera
    {
        private int _speed = 10;
        private Point _position = new Point();
        private CameraMotion _cameraMotion;

        public void Update()
        {
            Point cursor = GameCursor.Position;
            _cameraMotion = CameraMotion.None;
            if (cursor.X == 0)
            {
                _position.X -= _speed;
                _cameraMotion = CameraMotion.Left;
            }
            else if (cursor.X == Game.ScreenWidth - 1)
            {
                _position.X += _speed;
                _cameraMotion = CameraMotion.Right;
            }
            if (cursor.Y == 0)
            {
                _position.Y -= _speed;
                _cameraMotion = CameraMotion.Up;
            }
            else if (cursor.Y == Game.ScreenHeight - 1)
            {
                _position.Y += _speed;
                _cameraMotion = CameraMotion.Down;
            }
        }

        public bool CanSee(Rectangle rectangle)
        {
            Rectangle camera = new Rectangle(_position, Game.ScreenSize);
            return camera.IntersectsWith(rectangle);
        }

        public Point ScreenToPoint(Point point)
        {
            point = GameMath.Add(point, _position);
            return point;
        }

        public Point PointToScreen(Point point)
        {
            point = GameMath.Subtract(point, _position);
            return point;
        }

        public Point Position
        {
            get { return _position; }
        }

        public CameraMotion GetMotion()
        {
            return _cameraMotion;
        }
    }
}
