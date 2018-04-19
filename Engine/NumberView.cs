using System;
using System.Drawing;

namespace TheGame.Engine
{
    class NumberView
    {
        const string PATH = @"bitmaps\numbers\basic\";
        const string PNG = ".png";
        const string MINUS = "minus";
        const int MINUS_INDEX = 10;
        private static MovableBitmap[] _bitmaps = new MovableBitmap[11];
        private Size _size = new Size(16, 20);
        private int[] _value;
        private bool _isMinus;

        public NumberView()
        {
            for (int i = 0; i < MINUS_INDEX; i++)
            {
                _bitmaps[i] = LoadNumberBitmap(_bitmaps[i], i.ToString());
            }
            _bitmaps[MINUS_INDEX] = LoadNumberBitmap(_bitmaps[MINUS_INDEX], MINUS);
        }

        private MovableBitmap LoadNumberBitmap(MovableBitmap bitmap, string name)
        {
            if (bitmap != null)
                if (bitmap.IsLoaded)
                    return bitmap;
            bitmap = new MovableBitmap();
            bitmap.LoadBitmap(PATH + name + PNG);
            return bitmap;
        }

        public void Draw(Point position)
        {
            int count = 0;
            if (_isMinus)
            {
                _bitmaps[MINUS_INDEX].SetPosition(position + new Size(16 * count, 0));
                _bitmaps[MINUS_INDEX].Draw();
                count++;
            }
            for (int i = 0; i < _value.Length; i++)
            {
                _bitmaps[_value[i]].SetPosition(position + new Size(16 * (i + count), 0));
                _bitmaps[_value[i]].Draw();
            }
        }

        public int Value
        {
            set
            {
                _isMinus = value < 0;
                if (_isMinus)
                    value *= -1;
                string numbers = value.ToString();
                _value = new int[numbers.Length];
                for (int i = 0; i < _value.Length; i++)
                {
                    _value[i] = Int16.Parse(numbers[i].ToString());
                }
            }
        }
    }
}
