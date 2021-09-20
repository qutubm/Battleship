using Battleship.Enum;
using Battleship.Game.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class GameFacade
    {
        private static readonly object instanceLock = new object();
        private static GameFacade gameFacade;
        private Dictionary<Profile, GameAssistant> assistant;
        //private Dictionary<string, Dictionary<Profile, GameAssistant>> gameSessions;

        private GameFacade(){
            this.assistant = new Dictionary<Profile, GameAssistant>();
        }

        public static GameFacade GetInstance()
        {
            lock (instanceLock) 
            {
                if (gameFacade == null)
                {
                    gameFacade = new GameFacade();
                }
            }
            
            return gameFacade;
        }

        public void SetupProfile(string name1, string name2)
        {
            this.assistant.Add(Profile.PlayerOne, new GameAssistant(name1));
            this.assistant.Add(Profile.PlayerTwo, new GameAssistant(name2));
        }

        
        public CommandResult SetupShipLocations(Profile profileType, string offset, Alignment alignment, ShipType typeOfShip)
        {
            var check = CheckIfProfileIsSet();
            if (!check.Valid)
            {
                return check;
            }

            ICommand command = new SetShipCommand(offset, alignment, typeOfShip);
            var result = this.assistant[profileType].DoAction(command);
            return result;
        }

        public CommandResult Hit(Profile profileType, string coordinate)
        {
            var check = CheckIfProfileIsSet();
            if (!check.Valid)
            {
                return check;
            }

            ICommand command = new HitCommand(coordinate);
            var result = this.assistant[profileType].DoAction(command);
            return result;
        }

        public bool CheckIfPlayerHasLost(Profile profileType)
        {
            return this.assistant[profileType].IsLost();
        }

        private CommandResult CheckIfProfileIsSet()
        {
            CommandResult commandResult = new CommandResult();
            GameAssistant assistant1;
            GameAssistant assistant2;
            this.assistant.TryGetValue(Profile.PlayerOne, out assistant1);
            this.assistant.TryGetValue(Profile.PlayerTwo, out assistant2);
            if (assistant1 == null || assistant2 == null)
            {
                return new CommandResult { Valid = false, Message = "Please setup the profiles before proceeding." };
            }

            return new CommandResult { Valid = true };
        }


    }
}
