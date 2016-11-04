
namespace TheGame.RTS.Commands
{
    class FastRightClick : TargetCommand
    {
        const string FRC = "FastRightClickCommand";

        public FastRightClick()
            : base(new MovableBitmap(), FRC)
        {

        }

        public override Action Cast(Unit castUnit)
        {
            if (TargetUnit == null)
                return new Actions.Move(castUnit, TargetPoint);
            else if (TargetUnit != castUnit)
                return new Actions.Follow(castUnit, TargetUnit);
            else
                return base.Cast(castUnit);
        }
    }
}
