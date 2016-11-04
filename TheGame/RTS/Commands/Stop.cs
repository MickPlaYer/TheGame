
namespace TheGame.RTS.Commands
{
    class Stop : Command
    {
        public Stop(MovableBitmap buttonBitmap, string key)
            : base(buttonBitmap, key)
        {

        }

        public override Action Cast(Unit castUnit)
        {
            return new Actions.Stop(castUnit);
        }
    }
}
