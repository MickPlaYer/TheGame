using System.Drawing;

namespace TheGame.RTS.Actions
{
    class Attack : TargetAction
    {
        public Attack(Unit unit, Unit target)
            : base(target)
        {
            ExecuteUnit = unit;
        }

        public override void Execute()
        {
            base.SetAction(ExecuteUnit.Attack);
            base.Execute();
        }
    }
}
