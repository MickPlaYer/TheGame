
namespace TheGame.RTS.Actions
{
    class Stop : Action
    {
        public Stop(Unit unit)
        {
            ExecuteUnit = unit;
        }

        public override void Execute()
        {
            ExecuteUnit.Stop();
        }
    }
}
