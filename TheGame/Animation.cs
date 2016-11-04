using System.Collections.Generic;
using System.Drawing;
using System;

namespace TheGame
{
    class Animation
    {
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private Point _position;
        private int _interval;
        private int _intervalCount;
        private int _currentBitmap;

        // 新增一張圖片至動畫尾端
        public void AddBitmap(string bitmapPath)
        {
            _bitmaps.Add(new Bitmap(bitmapPath));
        }

        // 設定座標
        public void SetPosition(int x, int y)
        {
            _position = new Point(x, y);
        }

        // 更新動畫
        public void Move()
        {
            if (_intervalCount >= _interval)
            {
                _intervalCount = 0;
                _currentBitmap++;
                if (_currentBitmap >= _bitmaps.Count)
                    _currentBitmap = 0;
            }
            else
                _intervalCount++;
        }

        // 繪製動畫
        public void Show(Graphics graphics)
        {
            graphics.DrawImage(_bitmaps[_currentBitmap], _position);
        }

        // 動畫間隔:控製動畫切換速度
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Interval can't be a negative number.");
                _interval = value;
            }
        }

        // 回傳是否為動畫最開頭
        public bool IsBeginOfAnimation
        {
            get
            {
                return _currentBitmap == 0 && _intervalCount == 0;
            }
        }
    }
}
