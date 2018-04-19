using System.Collections.Generic;
using TheGame.Engine;

namespace TheGame.RTS.Units
{
    class ActionPriorityCompare : IComparer<Unit>
    {
        public int Compare(Unit UnitA, Unit UnitB)
        {
            int distanceA = 0;
            int distanceB = 0;
            if (!UnitA.IsDoingAction || !UnitB.IsDoingAction)
                return 0;
            if (UnitA.IsDoingAction)
                distanceA = (int)GameMath.Distance(UnitA.Position, UnitA.GetActionDestination());
            if (UnitB.IsDoingAction)
                distanceB = (int)GameMath.Distance(UnitB.Position, UnitB.GetActionDestination());
            return -distanceA + distanceB;
        }
    }
}
