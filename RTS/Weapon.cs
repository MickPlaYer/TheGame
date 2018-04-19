
namespace TheGame.RTS
{
    class Weapon
    {
        WeaponDataSet _data;
        private int _coolDown;

        public Weapon(WeaponDataSet data)
        {
            _data = data;
        }

        public void Update()
        {
            if (_coolDown > 0)
                _coolDown--;
        }

        public void Fire(Unit target)
        {
            if (_coolDown == 0)
            {
                _coolDown = _data.Interval;
                _data.Effect.Effective(target);
                //target.HitPoint.Value -= _data.Effect;
            }
        }

        public int Range { get { return _data.Range; } }
    }
}
