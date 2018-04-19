using System.Drawing;

namespace TheGame.RTS
{
    class UnitDataSet
    {
        const string NONAME = "noname";
        private string _name = NONAME;
        private Size _anchor = new Size();
        private int _radius;
        private int _hitpoint;
        private int _moveSpeed;
        private UnitView _unitView = UnitView.BasicView;
        private Command[,] _commands = new Command[0, 0];
        private WeaponDataSet _weapon;

        public void CreateUnitView()
        {
            _unitView = new UnitView(_name);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Size Anchor
        {
            get { return _anchor; }
            set { _anchor = value; }
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public int HitPoint
        {
            get { return _hitpoint; }
            set { _hitpoint = value; }
        }

        public int MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }

        public UnitView UnitView
        {
            get { return _unitView; }
            set { _unitView = value; }
        }

        public Command[,] Commands
        {
            get { return _commands; }
            set { _commands = value; }
        }

        public WeaponDataSet Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }
    }
}
