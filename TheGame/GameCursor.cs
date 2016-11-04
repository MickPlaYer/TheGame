using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TheGame
{
    public enum CursorType
    {
        Normal,
        NormalUnit,
        Target,
        TargetUnit,
        Left,
        Right,
        Up,
        Down
    }

    class GameCursor
    {
        const string PATH = @"bitmaps\cursor\";

        private static CursorType[,] decisionTable = { { CursorType.Normal, CursorType.NormalUnit }, { CursorType.Target, CursorType.TargetUnit } };
        private static CursorType _type = CursorType.Normal;
        private MovableBitmap[] _bitmaps = new MovableBitmap[Enum.GetValues(typeof(CursorType)).Length];
        private Size[] _anchor = { new Size(1, 1), new Size(1, 1), new Size(16, 16), new Size(16, 16), new Size(0, 17), new Size(31, 17), new Size(17, 0), new Size(17, 31) };
        private static Point _position = new Point();

        public GameCursor()
        {
            for (int i = 0; i < _bitmaps.Length; i++)
            {
                _bitmaps[i] = new MovableBitmap();
            }
            _bitmaps[0].LoadBitmap(PATH + "normal.png");
            _bitmaps[1].LoadBitmap(PATH + "nromal_unit.png");
            _bitmaps[2].LoadBitmap(PATH + "target.png");
            _bitmaps[3].LoadBitmap(PATH + "target_unit.png");
            _bitmaps[4].LoadBitmap(PATH + "side_left.png");
            _bitmaps[5].LoadBitmap(PATH + "side_right.png");
            _bitmaps[6].LoadBitmap(PATH + "side_top.png");
            _bitmaps[7].LoadBitmap(PATH + "side_buttom.png");
        }

        public void Update(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void Draw()
        {
            int index = (int)_type;
            _bitmaps[index].SetPosition(_position - _anchor[index]);
            _bitmaps[index].Draw();
        }

        public static void SetType(bool isTarget, bool isOnUnit)
        {
            _type = decisionTable[Convert.ToInt32(isTarget), Convert.ToInt32(isOnUnit)];
        }


        public static void SetType(CursorType type)
        {
            _type = type;
        }

        public static Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
