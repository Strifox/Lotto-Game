using Lotto_Game.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lotto_Game.Controllers
{
    public class GameManagerController
    {
        private NumberManagerController numberManagerController = new NumberManagerController();
        private PlayerManagerController playerManagerController = new PlayerManagerController();

        public bool isFinished;

        public void InitializeGame()
        {
            do
            {
                //Console.WriteLine("Tryck på 1 för att vara med på lotteriet.");
                //Generate players
                playerManagerController.players.Add(playerManagerController.GeneratePlayer());
                Console.Clear();
                Console.WriteLine("Tryck på '1' för att lägga till en spelare.");
                Console.WriteLine("Tryck på en annan knapp för att fortsätta.");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Tryck på '1' för att lägga till en spelare.");
                        Console.WriteLine("Tryck på en annan knapp för att fortsätta.");
                        Console.Clear();
                        break;
                    default:
                        isFinished = true;
                        break;
                }
            } while (!isFinished);

            // Sorts all players numbers before saving them.
            foreach (var player in playerManagerController.players)
            {
                numberManagerController.SortNumbers(player.Numbers);
            }

            // Saves players and their numbers into a textfile
            SavePlayerNumbersToTextfile(playerManagerController.players);

            // Generates the random numbers to be used in the game
            numberManagerController.GenerateRandomNumbers();

            // Sorts all the random generated numbers before writing them
            numberManagerController.SortNumbers(numberManagerController.AllLottoNumbers);

            // Writes all the numberes in one row as the correct row
            // e.g: Correct row: 7,8,9,10,11,12,15,21,22,23,24

            Console.Clear();

            Console.Write("Rätt rad: ");

            for (int i = 0; i < numberManagerController.AllLottoNumbers.Count; i++)
            {
                // Writes the last number without comma at the end
                if (i + 1 == numberManagerController.AllLottoNumbers.Count)
                    Console.Write($"{numberManagerController.AllLottoNumbers[i]}");
                // Writes all other numbers with comma
                else
                    Console.Write($"{numberManagerController.AllLottoNumbers[i]},");
            }


            foreach (var player in playerManagerController.players)
            {
                // Number of matches from the original 7 numbers
                var lottoNumberMatches = numberManagerController.MatchNumbers(player, numberManagerController.LottoNumbers);
                // Number of matches from the additional 4 numbers
                var additionalNumberMatches = numberManagerController.MatchNumbers(player, numberManagerController.AdditionalNumbers);
                Console.Write($"\n{player.Name}, ");

                for (int i = 0; i < player.Numbers.Count; i++)
                {
                    if (i + 1 == player.Numbers.Count)
                        Console.Write($"{player.Numbers[i]}");
                    else
                        Console.Write($"{player.Numbers[i]},");
                }

                SaveResultsToTextfile(playerManagerController.players);
                Console.Write($"\t\t{lottoNumberMatches} rätt, {additionalNumberMatches} tilläggsnummer");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Saves text into textfile as LottoNumbers.txt in MyDocuments
        /// </summary>
        /// <param name="players"></param>
        private void SavePlayerNumbersToTextfile(List<Player> players)
        {
            using (TextWriter tw = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Lottonumbers.txt"))
            {
                foreach (var player in players)
                {
                    // Writes the name to text file
                    tw.Write(string.Format("Namn: {0}, ", player.Name));

                    for (int i = 0; i < player.Numbers.Count; i++)
                    {
                        // Writes the last number without comma at the end
                        if (i + 1 == player.Numbers.Count)
                            tw.Write(string.Format("{0}", player.Numbers[i]));
                        // Writes all other numbers with comma
                        else
                            tw.Write(string.Format("{0},", player.Numbers[i]));
                    }
                    // Creates new line in text file after each player
                    tw.Write(string.Format("\n"));
                }
            }
        }

        /// <summary>
        /// Saves the results into a text file as LottoResults.txt in Mydocuments
        /// </summary>
        /// <param name="players"></param>
        private void SaveResultsToTextfile(List<Player> players)
        {
            using (TextWriter tw = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Lottoresults.txt"))
            {
                // Writes the correct row
                tw.Write(string.Format("Rätt rad: "));

                for (int i = 0; i < numberManagerController.AllLottoNumbers.Count; i++)
                {
                    // Writes the last number without comma at the end
                    if (i + 1 == numberManagerController.AllLottoNumbers.Count)
                        tw.Write(string.Format("{0}", numberManagerController.AllLottoNumbers[i]));
                    // Writes all other numbers with comma
                    else
                        tw.Write(string.Format("{0},", numberManagerController.AllLottoNumbers[i]));
                }

                foreach (var player in players)
                {
                    // Writes the name to text file
                    tw.Write(string.Format("\nNamn: {0}, ", player.Name));

                    // Number of matches from the original 7 numbers
                    var lottoNumberMatches = numberManagerController.MatchNumbers(player, numberManagerController.LottoNumbers);
                    // Number of matches from the additional 4 numbers
                    var additionalNumberMatches = numberManagerController.MatchNumbers(player, numberManagerController.AdditionalNumbers);

                    for (int i = 0; i < player.Numbers.Count; i++)
                    {
                        // Writes the last number without comma at the end
                        if (i + 1 == player.Numbers.Count)
                            tw.Write(string.Format("{0}", player.Numbers[i]));
                        // Writes all other numbers with comma
                        else
                            tw.Write(string.Format("{0},", player.Numbers[i]));
                    }

                    tw.Write(string.Format("\t\t{0} rätt, ", lottoNumberMatches));
                    tw.Write(string.Format("{0} tilläggsnummer", additionalNumberMatches));
                }
            }
        }
    }
}
