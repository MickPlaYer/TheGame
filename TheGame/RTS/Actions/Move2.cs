using System.Drawing;

namespace TheGame.RTS.Actions
{
    class Move2 : Action
    {
        Point _destination;

        public Move2(Unit executeUnit, Point destination)
        {
            ExecuteUnit = executeUnit;
            _destination = destination;
        }

        public override void Execute()
        {
            //_executeUnit.Move(_destination);
        }

        public override PointF Destination { get { return _destination; } }
    }
}
