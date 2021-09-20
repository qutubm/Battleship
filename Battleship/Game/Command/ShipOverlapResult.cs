using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Command
{
    public class ShipOverlapResult : CommandResult
    {
        public List<string> TotalCoordinates { get; set; }
    }
}
