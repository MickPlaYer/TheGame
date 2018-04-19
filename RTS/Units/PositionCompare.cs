using System.Collections.Generic;

namespace TheGame.RTS.Units
{
    class PositionCompare : IComparer<Unit>
    {
        public int Compare(Unit UnitA, Unit UnitB)
        {
            int aX = (int)UnitA.Position.X;
            int aY = (int)UnitA.Position.Y;
            int bX = (int)UnitB.Position.X;
            int bY = (int)UnitB.Position.Y;
            if (aY != bY)
                return aY - bY;
            else
                return aX - bX;
        }
    }
}
