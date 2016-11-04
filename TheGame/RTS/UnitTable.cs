using System.Collections.Generic;
using System.Drawing;

namespace TheGame.RTS
{
    class UnitTable
    {
        private List<Unit> _selectedUnits;
        private Point _position;
        private NumberView _hitpoint = new NumberView();

        public UnitTable(List<Unit> selectedUnits)
        {
            _selectedUnits = selectedUnits;
            _position = new Point(32, 32);
        }

        public void Draw()
        {
            if (_selectedUnits.Count != 0)
            {
                _selectedUnits[0].DrawSign(_position);
                _hitpoint.Value = _selectedUnits[0].HitPoint.Value;
                _hitpoint.Draw(_position + new Size(64, 10));
            }
        }
    }
}
