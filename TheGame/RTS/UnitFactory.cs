using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace TheGame.RTS
{
    class UnitFactory
    {
        const int BUTTON_ROW = 4;
        const int BUTTON_COLUMN = 3;
        private XDocument unitXmlDatas = XDocument.Load(@"data/unitdata.xml");
        private CommandFactory _commandFactory = new CommandFactory();
        private List<UnitDataSet> _units = new List<UnitDataSet>();
        private List<UnitView> _views = new List<UnitView>();

        public UnitFactory()
        {
            var unitXmlData = unitXmlDatas.Descendants("Unit");
            foreach (var unitElement in unitXmlData)
            {
                UnitDataSet dataSet = new UnitDataSet();
                var anchorElement = unitElement.Element("Anchor");
                var radiusElement = unitElement.Element("Radius");
                var moveSpeedElement = unitElement.Element("MoveSpeed");
                var hitpointElement = unitElement.Element("HitPoint");
                string width = anchorElement.Attribute("width").Value;
                string height = anchorElement.Attribute("height").Value;
                dataSet.Name = unitElement.Attribute("name").Value;
                dataSet.Anchor = new Size(int.Parse(width), int.Parse(height));
                dataSet.Radius = int.Parse(radiusElement.Attribute("value").Value);
                dataSet.HitPoint = int.Parse(hitpointElement.Attribute("value").Value);
                dataSet.MoveSpeed = int.Parse(moveSpeedElement.Attribute("value").Value);
                dataSet.Weapon = new WeaponDataSet(new Effects.Damage(1), 300, 60);
                dataSet.CreateUnitView();
                dataSet.Commands = _commandFactory.CreateBasicCommands();
                _units.Add(dataSet);
            }
        }

        public Unit CreateMarine()
        {
            return new Unit(_units[0]);
        }

        public Unit CreateTank()
        {
            return new Unit(_units[1]);
        }

        public Unit CreateUnit(string name)
        {
            return new Unit(_units.Find(unit => unit.Name == name));
        }
    }
}
