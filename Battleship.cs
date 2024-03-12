using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statki
{
    internal class Battleship
    {
        Player playerOne = new Player();
        Player playerTwo = new Player();
        
        public void Play()
        {
            Prepare();
            do
            {
                TitleScreen();
                Round();
            } while(PostRound());
            

        }
        public void Prepare()
        {
            Console.Clear();
            Console.WriteLine("  _________ __          __   __   .__  \r\n /   _____//  |______ _/  |_|  | _|__| \r\n \\_____  \\\\   __\\__  \\\\   __\\  |/ /  | \r\n /        \\|  |  / __ \\|  | |    <|  | \r\n/_______  /|__| (____  /__| |__|_ \\__| \r\n        \\/           \\/          \\/    ");
            Console.WriteLine("Ustaw nazwy graczy");
            Console.Write("Podaj nazwę pierwszego gracza: ");
            playerOne.SetName(Console.ReadLine());
            Console.Write("Podaj nazwę drugiego gracza: ");
            playerTwo.SetName(Console.ReadLine());
            if(playerOne.GetName().Length == 0 || playerTwo.GetName().Length == 0 || playerOne.GetName() == playerTwo.GetName())
            {
                Console.WriteLine("Podano niepoprawne nazwy graczy");
                Console.ReadKey();
                Prepare();
            }
        }
        public void TitleScreen()
        {
            Console.Clear();
            Console.WriteLine("  _________ __          __   __   .__  \r\n /   _____//  |______ _/  |_|  | _|__| \r\n \\_____  \\\\   __\\__  \\\\   __\\  |/ /  | \r\n /        \\|  |  / __ \\|  | |    <|  | \r\n/_______  /|__| (____  /__| |__|_ \\__| \r\n        \\/           \\/          \\/    ");
            Console.WriteLine("Wygrane:");
            Console.WriteLine($"{playerOne.GetName()}: {playerOne.GetWins()}");
            Console.WriteLine($"{playerTwo.GetName()}: {playerTwo.GetWins()}");
            Console.WriteLine("Naciśnij przycisk, aby kontynuować");
            Console.ReadKey();
        }
        public bool PostRound() 
        {
            Console.Clear();
            if(playerOne.IsWin())
            {
                Console.WriteLine($"Rundę wygrywa {playerOne.GetName()}");
            } else
            {
                Console.WriteLine($"Rundę wygrywa {playerOne.GetName()}");
            }
            do
            {
                Console.WriteLine("Rewanż?");
                Console.WriteLine("[1] - Tak");
                Console.WriteLine("[2] - Nie");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Wybrano Tak - rozpocznij rewanż.");
                        playerOne.Restart();
                        playerTwo.Restart();
                        return true;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Koinec gry");
                        return false;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Wybierz 1 lub 2.");
                        break;
                }
            } while (true);


        }
        public void Round()
        {
            playerOne.SetShips();
            ChangePlayer();
            playerTwo.SetShips();
            ChangePlayer();
            playerOne.SetEnemyShips(playerTwo.GetShips());
            playerTwo.SetEnemyShips(playerOne.GetShips());
            do
            {
                playerOne.Shot();
                if (CheckWin())
                {
                    break;
                }
                ChangePlayer();
                playerTwo.Shot();
            } while (!CheckWin());
        }
        public void ChangePlayer() 
        { 
            Console.Clear();
            Console.WriteLine("Zmiana Gracza");
            Console.ReadKey();
        }

        public bool CheckWin()
        {
            if(playerOne.IsWin())
            {
                return true;
            } else if (playerTwo.IsWin())
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
