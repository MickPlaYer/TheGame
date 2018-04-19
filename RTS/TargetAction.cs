using System.Drawing;

namespace TheGame.RTS
{
    class TargetAction : Action
    {
        public delegate void ActionFunction(Unit target);
        private Unit _targetUnit;
        private ActionFunction _action;

        public TargetAction(Unit target)
        {
            _targetUnit = target;
        }

        public override void Execute()
        {
            if (_targetUnit.IsAlive)
                _action(_targetUnit);
            else
                ExecuteUnit.Stop();
        }

        public void SetAction(ActionFunction action)
        {
            _action = action;
        }

        public override PointF Destination
        {
            get { return _targetUnit.Position; }
        }

        public Unit TargetUnit { get { return _targetUnit; } }
    }
}
