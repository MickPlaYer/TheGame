using TheGame.Engine;

namespace TheGame.RTS.Commands
{
    class Attack : TargetCommand
    {
        public Attack(MovableBitmap buttonBitmap, string key)
            : base(buttonBitmap, key)
        {

        }

        public override Action Cast(Unit castUnit)
        {
            if (TargetUnit == null)
                return new Actions.Move(castUnit, TargetPoint);
            else if (TargetUnit != castUnit)
                return new Actions.Attack(castUnit, TargetUnit);
            else
                return base.Cast(castUnit);
        }
    }
}
