
namespace TheGame.RTS
{
    class WeaponDataSet
    {
        private Effect _effect;
        private int _range;
        private int _interval;

        public WeaponDataSet(Effect effect, int range, int interval)
        {
            _effect = effect;
            _range = range;
            _interval = interval;
        }

        public Effect Effect
        {
            get { return _effect; }
            set { _effect = value; }
        }

        public int Range
        {
            get { return _range; }
            set { _range = value; }
        }

        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
    }
}
