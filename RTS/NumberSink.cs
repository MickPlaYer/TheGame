namespace TheGame.RTS
{
    class NumberSink
    {
        int _value;
        int _maxValue;

        public NumberSink(int value)
        {
            _value = _maxValue = value;
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value > _maxValue)
                    _value = _maxValue;
                else if (value < 0)
                    _value = 0;
                else
                    _value = value;
            }
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (MaxValue <= 0)
                    throw new System.Exception("MaxValue cant't less then 0.");
                if (value < _value)
                    _value = value;
                _maxValue = value;
            }
        }

        public bool IsEmpty { get { return _value == 0; } }
    }
}
