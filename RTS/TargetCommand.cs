using System.Drawing;
using TheGame.Engine;

namespace TheGame.RTS
{
    class TargetCommand : Command
    {
        private Point _targetPoint;
        private Unit _targetUnit;

        public TargetCommand(MovableBitmap buttonBitmap, string key)
            : base(buttonBitmap, key)
        {

        }

        public void SetTarget(Point targetPoint, Unit targetUnit)
        {
            _targetPoint = targetPoint;
            _targetUnit = targetUnit;
        }

        public override Action Cast(Unit castUnit)
        {
            return new Actions.Stop(castUnit);
        }

        public Point TargetPoint { get { return _targetPoint; } }

        public Unit TargetUnit { get { return _targetUnit; } }
    }
}
