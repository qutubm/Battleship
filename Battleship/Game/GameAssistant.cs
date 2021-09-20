using Battleship.Enum;
using Battleship.Game.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class GameAssistant
    {
        private Board board;
        private string profileName;

        public GameAssistant(string profileName)
        {
            this.profileName = profileName;
            board = new Board();
        }

        public CommandResult DoAction(ICommand command)
        {
            command.SetBoard(this.board);
            var result = command.ExecuteCommand();
            return result;
        }

        public bool IsLost()
        {
            var areAllShipsCapsized = true;

            foreach (var ship in this.board.Ships)
            {
                areAllShipsCapsized = areAllShipsCapsized && ship.Capsized;
            }

            this.board.AllShipsCapsized = areAllShipsCapsized;

            return this.board.AllShipsCapsized;
        }

    }
}
