using System.Drawing;

namespace TheGame.RTS.Actions
{
    class Follow : TargetAction
    {
        public Follow(Unit unit, Unit target)
            : base(target)
        {
            ExecuteUnit = unit;
        }

        public override void Execute()
        {
            base.SetAction(ExecuteUnit.Follow);
            base.Execute();
        }
    }
}
