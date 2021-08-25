using Lotto_Game.Controllers;
using System;
using System.Collections.Generic;

namespace Lotto_Game
{
    class Program
    {

        static void Main(string[] args)
        {
            GameManagerController gameManager = new GameManagerController();

            gameManager.InitializeGame();
        }
    }
}
