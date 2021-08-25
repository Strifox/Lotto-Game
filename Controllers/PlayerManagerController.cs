using Lotto_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_Game.Controllers
{
    public class PlayerManagerController
    {
        public List<Player> players = new List<Player>();

        public Player GeneratePlayer()
        {
            Console.WriteLine("Skriv in ditt namn");
            string playerName = Console.ReadLine();

            Console.WriteLine("Skriv in dina 7 lotto nummer med comma");
            string numbersInput = Console.ReadLine();
            try
            {
                // Splits and removes the commas before converting to string array.
                var inputSplitted = numbersInput.Trim().Split(',');
               
                // Converts input from string array to List<int>.
                var numbers = Array.ConvertAll(inputSplitted, int.Parse).ToList();
                
                if (numbers.Count != 7)
                    throw new FormatException();

                return new Player { Name = playerName, Numbers = numbers };
            }
            catch (Exception e)
            {
                Console.WriteLine("Dina nummer får inte sluta med comma och du måste skriva in 7 nummer", e.Message);
                throw new FormatException();
            }

        }
    }
}
