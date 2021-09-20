using Battleship.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class Ship
    {
        public Ship()
        {
            TotalCoordinates = new List<string>();
            HitsTaken = new List<string>();
        }

        public int Index { get; set; }
        public ShipType TypeOfShip { get; set; }
        public string StartCoordinate { get; set; }
        public string EndCoordinate { get; set; }
        public List<string> TotalCoordinates { get; set; }
        public List<string> HitsTaken { get; set; }
        public bool Capsized { get; set; } = false;
    }
}
