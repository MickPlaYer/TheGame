using System.Drawing;

namespace TheGame.RTS
{
    abstract class Action
    {
        private Unit _executeUnit;

        public abstract void Execute();

        public virtual PointF Destination { get { return new PointF(); } }

        public Unit ExecuteUnit
        {
            get { return _executeUnit; }
            set { _executeUnit = value; }
        }
    }
}
