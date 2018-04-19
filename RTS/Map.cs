using System.Collections.Generic;
using System.Drawing;
using TheGame.Engine;

namespace TheGame.RTS
{
    class Map
    {
        private static List<Unit> _units = new List<Unit>();
        private List<Unit> _selectedUnits = new List<Unit>();
        private Unit _cursorUnit = null;
        private UnitFactory _unitFactory = new UnitFactory();
        private List<MovableBitmap> _materialList = new List<MovableBitmap>();
        private Size _materialSize = new Size(128, 128);
        private Size _gridSize = new Size(16, 16);

        public Map()
        {
            MovableBitmap material = new MovableBitmap();
            material.LoadBitmap(@"bitmaps\terrain_material\metal.png");
            _materialList.Add(material);
        }

        private void CreateUnits()
        {
            for (int i = 0; i < 100; i++)
            {
                _units.Add(_unitFactory.CreateUnit("marine"));
                _units[i].Init(50 + 40 * (i / 10), 50 + 40 * (i % 10));
            }
            for (int i = 100; i < 110; i++)
            {
                _units.Add(_unitFactory.CreateUnit("tank"));
                _units[i].Init(-4950 + 50 * i + 100, 500);
            }
            for (int i = 110; i < 130; i++)
            {
                _units.Add(_unitFactory.CreateUnit("marauder"));
                _units[i].Init(-5450 + 50 * i + 110, 600);
            }
        }

        public void Init()
        {
            _units.Clear();
            CreateUnits();
        }

        public void Update()
        {
            _units.Sort(new Units.ActionPriorityCompare());
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].IsAlive)
                    _units[i].Update();
                else
                {
                    _selectedUnits.Remove(_units[i]);
                    _units.RemoveAt(i);
                }
            }
            #region Move 2.0 Fail
            /*bool isAllClear = false;
            while (!isAllClear)
            {
                isAllClear = true;
                for (int i = 0; i < _units.Count; i++)
                {
                    Unit unit = CollideCheck(_units[i], PointF.Empty);
                    if (unit != null)
                    {
                        isAllClear = false;
                        PointF vector = GameMath.Subtract(unit.Position, _units[i].Position);
                        vector = GameMath.Normalize(vector);
                        unit.Translate(vector);
                        _units[i].Translate(GameMath.Multiply(vector, -1));
                    }
                }
            }*/
            #endregion
        }

        public void Draw(Camera camera)
        {
            DrawTerrain(camera);
            DrawDistinationPoint(camera);
            DrawUnits(camera);
        }

        private void DrawUnits(Camera camera)
        {
            _units.Sort(new Units.PositionCompare());
            for (int i = 0; i < _units.Count; i++)
            {
                bool isDrawOutline = _selectedUnits.Contains(_units[i]) || _units[i] == _cursorUnit;
                if (camera.CanSee(_units[i].GetUnitRegion()))
                    _units[i].Draw(camera, isDrawOutline);
            }
        }

        private void DrawDistinationPoint(Camera camera)
        {
            for (int i = 0; i < _selectedUnits.Count; i++)
            {
                if (_selectedUnits[i].IsDoingAction)
                {
                    Point point = camera.PointToScreen(_selectedUnits[i].GetActionDestination());
                    Brush brush = new SolidBrush(Color.FromArgb(0, 255, 0));
                    Game.Graphics.FillRectangle(brush, new Rectangle(point - new Size(5, 5), new Size(10, 10)));
                    brush.Dispose();
                    break;
                }
            }
        }

        private void DrawTerrain(Camera camera)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Point point = new Point(i * _materialSize.Width, j * _materialSize.Height);
                    if (camera.CanSee(new Rectangle(point, _materialSize)))
                    {
                        _materialList[0].SetPosition(camera.PointToScreen(point));
                        _materialList[0].Draw();
                    }
                }
            }
        }

        public void OnMouseMove(Point point)
        {
            _cursorUnit = null;
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].IsBeingChosen(new Rectangle(point.X, point.Y, 1, 1)))
                {
                    _cursorUnit = _units[i];
                    return;
                }
            }
        }

        public static Unit CollideCheck(Unit unit, PointF vector)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i] != unit)
                {
                    double length = GameMath.Distance(_units[i].Position, GameMath.Add(unit.Position, vector));
                    double radiusAnd = _units[i].Radius + unit.Radius;
                    if (length <= radiusAnd)
                        return _units[i];
                }
            }
            return null;
        }

        public static Unit CollideCheck(PointF destination)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                double length = GameMath.Distance(_units[i].Position, destination);
                if (length <= _units[i].Radius)
                        return _units[i];
            }
            return null;
        }

        public static bool CollideCheck(Unit unitA, Unit unitB)
        {
            double length = GameMath.Distance(unitA.Position, unitB.Position);
            double radiusAnd = unitA.Radius + unitB.Radius;
            return length <= radiusAnd;
        }

        public List<Unit> UnitList { get { return _units; } }

        public List<Unit> SelectedUnitList { get { return _selectedUnits; } }

        public Unit CursorUnit { get { return _cursorUnit; } }
    }
}
