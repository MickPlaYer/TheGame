namespace TheGame.RTS.Effects
{
    class Damage : Effect
    {
        int _value;

        public Damage(int value)
        {
            _value = value;
        }

        public override void Effective(Unit unit)
        {
            unit.HitPoint.Value -= _value;
        }
    }
}
