using Battleship.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Command
{
    public class SetShipCommand : Command, ICommand
    {
        //private Board board;
        private string offsetCoordinate;
        private Alignment alignment;
        private ShipType shipType;

        public SetShipCommand(string offsetCoordinate, Alignment alignment, ShipType shipType) 
        {
            //this.board = board;
            this.offsetCoordinate = offsetCoordinate;
            this.alignment = alignment;
            this.shipType = shipType;
        }

        public void SetBoard(Board board)
        {
            this.board = board;
        }

        public CommandResult ExecuteCommand()
        {
            var result = SetShipCoordinates();

            return result;
        }

        // string startCoordinate, string endCoordinate
        private CommandResult SetShipCoordinates()
        {
            if (this.board.Ships == null)
            {
                this.board.Ships = new List<Ship>();
            }

            var commandResult = new CommandResult();
            var startCoordinate = this.offsetCoordinate;

            // Check if the Ship's start co-ordinate is within bounds
            var startCoordinateResult = CheckCoordinateForBounds(startCoordinate);
            if (!startCoordinateResult.Valid)
            {
                commandResult.Valid = false;
                commandResult.Message = "Start Co-Ordinate of the Ship is Out of Bounds";
                return commandResult;
            }

            string endCoordinate = SetEndCoordinate();

            // Check if the Ship's end co-ordinate is within bounds
            var endCoordinateResult = CheckCoordinateForBounds(endCoordinate);
            if (!endCoordinateResult.Valid)
            {
                commandResult.Valid = false;
                commandResult.Message = "End Co-Ordinate of the Ship is Out of Bounds";
                return commandResult;
            }

            // Check if the Ship is either horizontally placed or vertically placed
            var startCoordinateBreakup = startCoordinate.Split("-");
            var endCoordinateBreakup = endCoordinate.Split("-");
            if ((startCoordinateBreakup[0] != endCoordinateBreakup[0]) && (startCoordinateBreakup[1] != endCoordinateBreakup[1]))
            {
                commandResult.Valid = false;
                commandResult.Message = "Ship is placed diagonally which is not allowed.";
                return commandResult;
            }

            // Check Overlap of the Ship Co-ordinates with other ships
            var shipOverlapResult = CheckShipOverlap(startCoordinate, endCoordinate);
            if (!shipOverlapResult.Valid)
            {
                return shipOverlapResult;
            }

            // When no issues then save the Ship's Coordinates
            this.board.Ships.Add(new Ship { Index = this.board.Ships.Count() + 1, StartCoordinate = startCoordinate, EndCoordinate = endCoordinate, TotalCoordinates = shipOverlapResult.TotalCoordinates });

            return shipOverlapResult;
        }

        private ShipOverlapResult CheckShipOverlap(string startCoordinate, string endCoordinate)
        {
            var shipOverlapResult = new ShipOverlapResult();

            var totalCoordinates = CalculateTotalShipCoordinates(startCoordinate, endCoordinate);

            if (this.board.Ships == null || this.board.Ships.Count == 0)
            {
                shipOverlapResult.Valid = true;
                shipOverlapResult.TotalCoordinates = totalCoordinates;
                return shipOverlapResult;
            }

            foreach (var ship in this.board.Ships)
            {
                var intersect = totalCoordinates.Intersect(ship.TotalCoordinates).ToList();

                if (intersect != null && intersect.Count > 0)
                {
                    shipOverlapResult.Valid = false;
                    shipOverlapResult.Message = $"Newly entered Ship Location is overlapping with existing ships on {string.Join(", ", intersect)}.";
                    return shipOverlapResult;
                }
            }

            shipOverlapResult.Valid = true;
            shipOverlapResult.TotalCoordinates = totalCoordinates;
            return shipOverlapResult;
        }

        private List<string> CalculateTotalShipCoordinates(string startCoordinate, string endCoordinate)
        {
            var startCoordinateBreakup = startCoordinate.Split("-");
            var endCoordinateBreakup = endCoordinate.Split("-");

            var totalCoordinates = new List<string>();
            if (startCoordinateBreakup[0] == endCoordinateBreakup[0])
            {
                for (int counter = int.Parse(startCoordinateBreakup[1]); counter <= int.Parse(endCoordinateBreakup[1]); counter++)
                {
                    totalCoordinates.Add(startCoordinateBreakup[0] + "-" + counter.ToString());
                }
            }
            else
            {
                for (int counter = this.board.XAxisAllowedInputs.IndexOf(startCoordinateBreakup[0]); counter <= this.board.XAxisAllowedInputs.IndexOf(endCoordinateBreakup[0]); counter++)
                {
                    totalCoordinates.Add(this.board.XAxisAllowedInputs[counter] + "-" + startCoordinateBreakup[1]);
                }
            }

            return totalCoordinates;
        }

        private string SetEndCoordinate()
        {
            var offsetBreakup = this.offsetCoordinate.Split("-");
            
            var xAxisValue = offsetBreakup[0];
            var yAxisValue = int.Parse(offsetBreakup[1]);

            if (this.alignment == Alignment.Horizontal)
            {
                for (int counter = 0; counter < (int)this.shipType - 1; counter++)
                {
                    yAxisValue++;
                }
            }
            else 
            {
                var xAxisPosition = this.board.XAxisAllowedInputs.IndexOf(xAxisValue);
                for (int counter = 0; counter < (int)this.shipType - 1; counter++)
                {
                    xAxisPosition++;
                }
                xAxisValue = this.board.XAxisAllowedInputs[xAxisPosition];
            }

            return xAxisValue + "-" + yAxisValue;
        }



    }
}
