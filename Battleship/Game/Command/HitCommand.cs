using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Command
{
    public class HitCommand : Command, ICommand
    {
        //private Board board;
        private string coordinate;

        public HitCommand( string coordinate) 
        {
            //this.board = board;
            this.coordinate = coordinate;
        }

        public void SetBoard(Board board)
        {
            this.board = board;
        }

        public CommandResult ExecuteCommand()
        {
            return RegisterHit();
        }

        public CommandResult RegisterHit()
        {
            var result = new CommandResult();

            if (this.board.Ships == null || this.board.Ships.Count() == 0)
            {
                result.Valid = false;
                result.Message = "Ships have not been set, cannot register a hit.";
                return result;
            }

            // Check if the co-ordinate is within bounds
            var coordinateResult = CheckCoordinateForBounds(coordinate);
            if (!coordinateResult.Valid)
            {
                result.Message = "Hit Co-Ordinate is Out of Bounds";
                return result;
            }


            // 
            if (this.board.Attacks.Contains(coordinate))
            {
                result.Message = "This hit has already been made. Try another Co-ordinate";
                return result;
            }

            this.board.Attacks.Add(coordinate);

            foreach (var ship in this.board.Ships.Where(x => x.Capsized == false))
            {
                if (ship.TotalCoordinates.Contains(coordinate))
                {
                    ship.HitsTaken.Add(coordinate);

                    // Check if the Ship has been Capsized
                    ship.Capsized = ship.TotalCoordinates.Intersect(ship.HitsTaken).Count() == ship.TotalCoordinates.Count();
                    result.Valid = true;
                    result.Message = $"Ship No. {ship.Index} has been hit.";
                    result.Message += ship.Capsized ? " It has also been Capsized." : string.Empty;
                    return result;
                }
            }

            result.Valid = true;
            result.Message = "You missed it.";
            return result;
        }


    }
}
