using System.Drawing;

namespace TheGame.RTS.Actions
{
    class Move : Action
    {
        private PointF _destination;

        public Move(Unit unit, PointF destination)
        {
            _destination = destination;
            ExecuteUnit = unit;
        }

        public override void Execute()
        {
            ExecuteUnit.Move(_destination);
        }

        public override PointF Destination
        {
            get { return _destination; }
        }
    }
}
