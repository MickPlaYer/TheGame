using Microsoft.DirectX.DirectDraw;
using System;
using System.Drawing;

namespace TheGame
{
    class MovableBitmap : IDisposable
    {
        private Bitmap _bitmap;
        private Point _position;
        private bool _isLoaded = false;

        public void LoadBitmap(string bitmapPath)
        {
            if (_isLoaded)
                return;
            try
            {
                _bitmap = new Bitmap(bitmapPath);
            }
            catch
            {
                throw new Exception("Load bitmap fail: " + bitmapPath);
            }
            _isLoaded = true;
        }

        public void SetBitmap(Bitmap bitmap)
        {
            if (_isLoaded)
                return;
            _bitmap = bitmap;
            _isLoaded = true;
        }

        public void SetPosition(int x, int y)
        {
            _position = new Point(x, y);
        }

        public void SetPosition(Point position)
        {
            _position = position;
        }

        public void Draw()
        {
            Game.Graphics.DrawImage(_bitmap, _position);
        }

        public void Dispose()
        {
            _bitmap.Dispose();
        }

        public bool IsLoaded { get { return _isLoaded; } }
    }
}
