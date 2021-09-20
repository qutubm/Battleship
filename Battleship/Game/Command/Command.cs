using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship.Game.Command
{
    public abstract class Command
    {
        protected Board board;

        protected CommandResult CheckCoordinateForBounds(string coordinate)
        {
            // Check if Board is Setup
            var boardChecking = IsBoardSetup();
            if (!boardChecking.Valid) return boardChecking;

            var result = coordinate.Split("-");

            if (result.Length != 2)
            {
                return new CommandResult { Valid = false, Message = "Entered Coordinate is not valid, please try again." };
            }

            var xAxisValidity = CheckXAxisValidity(result[0]);
            if (!xAxisValidity.Valid)
            {
                return xAxisValidity;
            }

            var yAxisValidity = CheckYAxisValidity(result[1]);
            if (!yAxisValidity.Valid)
            {
                return yAxisValidity;
            }

            return new CommandResult { Valid = true };
        }

        private CommandResult IsBoardSetup()
        {
            var commandResult = new CommandResult();

            if (this.board.XAxisBound == 0 || this.board.YAxisBound == 0)
            {
                commandResult.Valid = false;
                commandResult.Message = "The Board is not setup.";
                return commandResult;
            }

            commandResult.Valid = true;
            return commandResult;
        }

        private CommandResult CheckXAxisValidity(string xAxisValue)
        {
            Regex r = new Regex("^[A-Z]+$");
            var isXCoordinateValid = r.IsMatch(xAxisValue);
            if (!isXCoordinateValid)
            {
                return new CommandResult { Valid = false, Message = "X Axis Co-ordinate is not alphabetical." };
            }

            if (!this.board.XAxisAllowedInputs.Contains(xAxisValue))
            {
                return new CommandResult { Valid = false, Message = $"X Axis Co-ordinate is out of bounds. It should be between A and {this.board.NumberToString(this.board.XAxisBound)}." };
            }

            return new CommandResult { Valid = true };
        }

        private CommandResult CheckYAxisValidity(string yAxisValue)
        {
            int yCoordinate;
            var isYCoordinateValid = int.TryParse(yAxisValue, out yCoordinate);
            if (!isYCoordinateValid)
            {
                return new CommandResult { Valid = false, Message = "Y Axis Co-ordinate is not a valid number." };
            }

            if (yCoordinate <= 0 && yCoordinate > this.board.XAxisBound)
            {
                return new CommandResult
                {
                    Valid = false,
                    Message = $"Y Axis Co-ordinate is out of bounds. It should be between {1} and {this.board.YAxisBound}."
                };
            }

            return new CommandResult { Valid = true };
        }

    }
}
