using Lotto_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_Game.Controllers
{
    public class NumberManagerController
    {
        public List<int> LottoNumbers = new List<int>();
        public List<int> AdditionalNumbers = new List<int>();
        public List<int> AllLottoNumbers = new List<int>();

        /// <summary>
        /// Generates one random number
        /// </summary>
        /// <returns></returns>
        private int GenerateRandomNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 36);

            return number;
        }

        /// <summary>
        /// Generates and adds random numbers to the lists used in the game
        /// </summary>
        /// <param name="numbersList">Referens to list for store numbers</param>
        /// <param name="amountOfNumbers">Amount of numbers you want to generade</param>
        public void GenerateRandomNumbers()
        {

            for (int i = 1; i <= 11; i++)
            {
                newnumber:
                int number = GenerateRandomNumber();

                // Checks if any number already exist
                // if it does, it generates a new number
                if (AllLottoNumbers.Contains(number))
                    goto newnumber;

                // Adds the first 7 numbers as lotto numbers (correct ones)
                if (i <= 7)
                {
                    
                    LottoNumbers.Add(number);
                    // Adds the same number to specific list which
                    // contains all numbers
                    AllLottoNumbers.Add(number);
                }
                // Adds the last 4 numbers as Additional numbers
                else if (i > 7)
                {
                    AdditionalNumbers.Add(number);
                    // Adds the same number to specific list which
                    // contains all numbers
                    AllLottoNumbers.Add(number);
                }
            }
        }

        public void SortNumbers(List<int> numbers)
        {
            numbers.Sort();
        }

        public int MatchNumbers(Player player, List<int> numbers)
        {
            int matches = 0;

            foreach (var playerNumber in player.Numbers)
            {
                foreach (var lottoNumber in numbers)
                {
                    if (lottoNumber == playerNumber)
                        matches++;
                }
            }

            return matches;
        }

    }
}
