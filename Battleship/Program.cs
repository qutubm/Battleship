using Battleship.Enum;
using Battleship.Game;
using Battleship.Game.Command;
using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        private static GameFacade gameFacade;
        private static string nameOfPlayerOne;
        private static string nameOfPlayerTwo;

        static void Main(string[] args)
        {
            gameFacade = GameFacade.GetInstance();

            Console.WriteLine("This is a 2 player BattleShip game.");
            Console.WriteLine("Each player gets to place 3 ships.{0}", Environment.NewLine);

            gameFacade.SetupProfile(nameOfPlayerOne, nameOfPlayerTwo);

            SetupShipLocations(Profile.PlayerOne, gameFacade);
            Console.Clear();
            SetupShipLocations(Profile.PlayerTwo, gameFacade);
            Console.Clear();

            var playerOneHasWon = false;
            var playerTwoHasWon = false;
            while (!(playerOneHasWon || playerTwoHasWon))
            {
                Attack(Profile.PlayerOne, Profile.PlayerTwo, gameFacade);
                playerOneHasWon = gameFacade.CheckIfPlayerHasLost(Profile.PlayerTwo);

                if (!playerOneHasWon)
                {
                    Attack(Profile.PlayerTwo, Profile.PlayerOne, gameFacade);
                    playerTwoHasWon = gameFacade.CheckIfPlayerHasLost(Profile.PlayerOne);
                }
            }

            Console.WriteLine(Environment.NewLine);

            if (playerOneHasWon)
            { 
                Console.WriteLine("{0} has Won.", Profile.PlayerOne);
            }

            if (playerTwoHasWon)
            {
                Console.WriteLine("{0} has Won.", Profile.PlayerTwo);
            }

            Console.WriteLine(Environment.NewLine); 
            Console.WriteLine("Game Ends!");
        }

        private static void SetupShipLocations(Profile profileType, GameFacade gameFacade)
        {
            var numberOfShipsToBeSet = 3;
            var shipSetCounter = 0;
            string offset = string.Empty;
            string alignmentOfShip = "";
            var shipTypes = new Dictionary<int, Tuple<ShipType, string>>();
            shipTypes[0] = new Tuple<ShipType, string>(ShipType.Carrier, "Carrier");
            shipTypes[1] = new Tuple<ShipType, string>(ShipType.Destroyer, "Destroyer");
            shipTypes[2] = new Tuple<ShipType, string>(ShipType.Submarine, "Submarine");
            var shipAlignment = new Dictionary<string, Tuple<Alignment, string>>();
            shipAlignment["1"] = new Tuple<Alignment, string>(Alignment.Horizontal, "Horizontal");
            shipAlignment["2"] = new Tuple<Alignment, string>(Alignment.Vertical, "Vertical");
            CommandResult commandResult;

            while (shipSetCounter < numberOfShipsToBeSet)
            {
                commandResult = new CommandResult();

                Console.WriteLine("Please enter details for Ship {0} for {1} {2}", shipTypes[shipSetCounter].Item2, profileType, Environment.NewLine);

                Console.WriteLine("Please enter Offset of {0} (Ex: A-1, B-3, etc):", shipTypes[shipSetCounter].Item2);
                offset = Console.ReadLine();

                while (!(alignmentOfShip == "1" || alignmentOfShip == "2"))
                {
                    Console.WriteLine("Please enter Alignment of {0} (1 for horizontal and 2 for Vertical, no other values will be accepted):", shipTypes[shipSetCounter].Item2, Environment.NewLine);
                    alignmentOfShip = Console.ReadLine();
                }

                commandResult = gameFacade.SetupShipLocations(profileType, offset, shipAlignment[alignmentOfShip].Item1, shipTypes[shipSetCounter].Item1);

                if (commandResult.Valid)
                {
                    alignmentOfShip = "";
                    shipSetCounter++;
                }
                else
                {
                    Console.Write("{0}. Please try again. {1}{2}", commandResult.Message, Environment.NewLine, Environment.NewLine);
                }
            }

            Console.Write("Ship details for {0} has been collected. Enter any button to continue...", profileType);
            Console.ReadLine();
        }

        private static void Attack(Profile attackProfileType, Profile defendProfileType, GameFacade gameFacade)
        {
            string coordinate = string.Empty;
            CommandResult commandResult = new CommandResult();

            while (!commandResult.Valid)
            {
                Console.WriteLine(attackProfileType);
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Please enter Co-ordinate for Attack (Ex: A-1, B-3, etc) for {0}:", attackProfileType);
                coordinate = Console.ReadLine();
                commandResult = gameFacade.Hit(defendProfileType, coordinate);
                if (!commandResult.Valid)
                {
                    Console.Write("{0}. Please try again. {1}{2}", commandResult.Message, Environment.NewLine, Environment.NewLine);
                }
                else
                {
                    Console.Write(commandResult.Message);
                }
            }

            Console.WriteLine(Environment.NewLine);
        }


    }
}
