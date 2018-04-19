using System.Drawing;
using System.Collections.Generic;
using TheGame.Engine;

namespace TheGame.RTS
{
    class Unit
    {
        private const int STUCK = 40;
        private const int ACTION_MAX = 16;
        private string _name;
        private UnitView _unitView;
        private Size _anchor;
        private int _radius;
        private NumberSink _hitpoint;
        private int _moveSpeed;
        private Command[,] _commands;
        private Weapon _weapon;
        private PointF _position;
        private List<Action> _actionDoing = new List<Action>();
        private int _stuckCount;

        public Unit(UnitDataSet dataSet)
        {
            _name = dataSet.Name;
            _unitView = dataSet.UnitView;
            _anchor = dataSet.Anchor;
            _radius = dataSet.Radius;
            _hitpoint = new NumberSink(dataSet.HitPoint);
            _moveSpeed = dataSet.MoveSpeed;
            _commands = dataSet.Commands;
            _weapon = new Weapon(dataSet.Weapon);
        }

        public void Init(int x, int y)
        {
            _actionDoing.Clear();
            SetPoisition(x, y);
        }

        public void Update()
        {
            if (_actionDoing.Count != 0)
                _actionDoing[0].Execute();
            _weapon.Update();
        }

        public void Draw(Camera camera, bool isDrawOutline)
        {
            Point drawPoint = Point.Truncate(_position) - _anchor;
            drawPoint = camera.PointToScreen(drawPoint);
            _unitView.Bitmap.SetPosition(drawPoint);
            _unitView.Bitmap.Draw();
            if (isDrawOutline)
            {
                _unitView.Outline.SetPosition(drawPoint);
                _unitView.Outline.Draw();
            }
        }

        public void DrawOutline()
        {
            _unitView.Outline.SetPosition(Point.Truncate(_position) - _anchor);
            _unitView.Outline.Draw();
        }

        public void DrawSign(Point poisition)
        {
            _unitView.Sign.SetPosition(poisition);
            _unitView.Sign.Draw();
        }

        public void SetPoisition(int x, int y)
        {
            _position = new Point(x, y);
        }

        public void SetPoisition(float x, float y)
        {
            _position = new PointF(x, y);
        }

        public bool IsBeingChosen(Rectangle choseBox)
        {
            Rectangle unitRegion = GetUnitRegion();
            return unitRegion.IntersectsWith(choseBox);
        }

        public void CommandUnit(Action action)
        {
            if (!KeyboardInput.IsKeyDown(KeyCodes.ShiftKey))
            {
                _actionDoing.Clear();
                _stuckCount = 0;
            }
            if (_actionDoing.Count < ACTION_MAX)
                _actionDoing.Add(action);
        }

        public void Stop()
        {
            _actionDoing.RemoveAt(0);
            _stuckCount = 0;
        }

        public void Attack(Unit target)
        {
            float distance = GameMath.Distance(_position, target.Position);
            distance -= (_radius + target.Radius);
            if (distance > _weapon.Range)
                Follow(target);
            else
                _weapon.Fire(target);
        }

        public void Move(PointF destination)
        {
            PointF vector = GetVector(destination);
            PointF direction = GetMoveDirection(vector, destination);
            if (destination != PointF.Empty)
                Translate(direction);
            if (direction != vector)
                _stuckCount++;
            else
                _stuckCount--;
            float distance = GameMath.Distance(Position, destination);
            if (_stuckCount > STUCK || distance < MoveSpeed)
                Stop();
        }

        public void Follow(Unit target)
        {
            PointF vector = GetVector(target.Position);
            PointF direction = GetMoveDirection(vector, target.Position);
            Unit frontUnit = Map.CollideCheck(this, vector);
            if (vector == direction)
                Translate(direction);
            else if (frontUnit != target)
                Translate(direction);
        }

        private PointF GetVector(PointF point)
        {
            PointF vector = GameMath.Subtract(point, _position);
            vector = GameMath.Normalize(vector);
            vector = GameMath.Multiply(vector, _moveSpeed);
            return vector;
        }

        private PointF GetMoveDirection(PointF vector, PointF destination)
        {
            Unit frontUnit = Map.CollideCheck(this, vector);
            if (frontUnit == null)
                return vector;
            else
            {
                PointF vectorLeft = new PointF(vector.Y, -vector.X);
                PointF vectorRight = new PointF(-vector.Y, vector.X);
                Unit LeftUnit = Map.CollideCheck(this, vectorLeft);
                Unit RightUnit = Map.CollideCheck(this, vectorRight);
                float distanceLeft = GameMath.Distance(GameMath.Add(_position, vectorLeft), destination);
                float distanceRight = GameMath.Distance(GameMath.Add(_position, vectorRight), destination);
                if (LeftUnit == null && RightUnit == null)
                {
                    if (distanceLeft <= distanceRight)
                        return vectorLeft;
                    else
                        return vectorRight;
                }
                else if (LeftUnit == null)
                    return vectorLeft;
                else if (RightUnit == null)
                    return vectorRight;
                else
                    return PointF.Empty;
            }
        }

        public Rectangle GetUnitRegion()
        {
            return new Rectangle(Point.Truncate(_position) - new Size(_radius, _radius), new Size(_radius * 2, _radius * 2));
        }

        public Point GetActionDestination()
        {
            if (_actionDoing.Count == 0)
                return Point.Empty;
            return Point.Truncate(_actionDoing[0].Destination);
        }

        public string Name { get { return _name; } }

        public int MoveSpeed { get { return _moveSpeed; } }

        public PointF Position { get { return _position; } }

        public int Radius { get { return _radius; } }

        public Command[,] Commands { get { return _commands; } }

        public NumberSink HitPoint { get { return _hitpoint; } }

        public bool IsDoingAction
        {
            get { return _actionDoing.Count != 0; }
        }

        /*public void Move(PointF destination)
        {
            float distance = GameMath.Distance(_position, destination);
            if (distance < _moveSpeed || distance > _oldDistance)
                Stop();
            //_oldDistance = distance;
            PointF vector = GameMath.Normalize(new PointF(destination.X - _position.X, destination.Y - _position.Y));
            vector = GameMath.Multiply(vector, _moveSpeed);
            Translate(vector);
        }*/

        public void Translate(PointF vector)
        {
            _position = GameMath.Add(_position, vector);
        }

        public bool IsMoving
        {
            get
            {
                if (_actionDoing.Count == 0)
                    return false;
                else
                    return !(_actionDoing[0] is Actions.Stop);
            }
        }

        public bool IsAlive { get { return !_hitpoint.IsEmpty; } }
    }
}
