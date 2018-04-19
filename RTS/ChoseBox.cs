using System;
using System.Collections.Generic;
using System.Drawing;
using TheGame.Engine;

namespace TheGame.RTS
{
    class ChoseBox : IDisposable
    {
        private Pen _pen = new Pen(Color.FromArgb(0, 255, 0));
        private Rectangle _box;
        private Point _point = new Point(-1, -1);

        public void Draw()
        {
            Game.Graphics.DrawRectangle(_pen, _box);
        }

        public void NewBox(Point point)
        {
            _box = new Rectangle();
            _point = point;
        }

        public void SetBox(int x, int y)
        {
            _box = new Rectangle(_point.X, _point.Y, Math.Abs(_point.X - x), Math.Abs(_point.Y - y));
            if (_point.X > x)
                _box.X = x;
            if (_point.Y > y)
                _box.Y = y;
        }

        public void ChoseUnits(List<Unit> units, List<Unit> selectedUnits, Camera camera)
        {
            Point point = camera.ScreenToPoint(new Point(_box.X, _box.Y));
            Size size = _box.Size;
            Rectangle box = new Rectangle(point, size);
            List<Unit> newUnitList = new List<Unit>();
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].IsBeingChosen(box))
                    newUnitList.Add(units[i]);
            }
            if (newUnitList.Count == 1 && KeyboardInput.IsKeyDown(KeyCodes.ControlKey))
                for (int i = 0; i < units.Count; i++)
                {
                    if (camera.CanSee(units[i].GetUnitRegion()) && units[i].Name == newUnitList[0].Name)
                        newUnitList.Add(units[i]);
                }
            Cancle();
            if (newUnitList.Count != 0)
            {
                selectedUnits.Clear();
                selectedUnits.AddRange(newUnitList);
            }
        }

        public void Cancle()
        {
            _point = new Point(-1, -1);
        }

        public bool IsChosing { get { return _point != new Point(-1, -1); } }

        public void Dispose()
        {
            _pen.Dispose();
        }
    }
}
