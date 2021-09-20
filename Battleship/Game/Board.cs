using Battleship.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class Board
    {
        public Board()
        {
            XAxisBound = 10;
            YAxisBound = 10;
            SetupXAxisAllowedAlphabets(XAxisBound);
            NumberOfShips = 3;
            Attacks = new List<string>();
        }

        public Profile ProfileType { get; set; }
        public int XAxisBound { get; set; }
        public int YAxisBound { get; set; }
        public int NumberOfShips { get; set; }
        public List<string> XAxisAllowedInputs { get; set; }
        public List<Ship> Ships { get; set; }
        public List<string> Attacks { get; set; } 
        public bool AllShipsCapsized { get; set; }

        private void SetupXAxisAllowedAlphabets(int xBlockCount)
        {
            XAxisAllowedInputs = new List<string>();

            for (int counter = 1; counter <= xBlockCount; counter++)
            {
                XAxisAllowedInputs.Add(NumberToString(counter));
            }
        }

        public string NumberToString(int value) 
        {
            StringBuilder sb = new StringBuilder();

            do
            {
                value--;
                int remainder = 0;
                value = Math.DivRem(value, 26, out remainder);
                sb.Insert(0, Convert.ToChar('A' + remainder));

            } while (value > 0);

            return sb.ToString();
        }


    }
}
