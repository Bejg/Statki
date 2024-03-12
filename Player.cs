using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statki
{
    internal class Player
    {
        private Board board = new Board();
        private Board enemyBoard = new Board();
        private List<Ship> ships = new List<Ship>();
        private List<Ship> enemyShips = new List<Ship>();
        private string name;
        private int wins = 0;

        public void SetEnemyShips(List<Ship> enemyShips) 
        { 
            this.enemyShips = enemyShips;
        }

        public List<Ship> GetShips()
        {
            return ships;
        }
        public void DisplayBoard()
        {
            Console.WriteLine("Plansza gracza");
            board.PrintBoard();
            Console.WriteLine("Plansza przeciwnika");
            enemyBoard.PrintBoard();
        }

        public void SetName(string name) { this.name = name;}
        public string GetName() { return name;}
        public void SetupShips(string shipName, int shipCount, int connectedCount)
        {
            Console.Clear();
            Console.WriteLine("Ustaw swoje statki");
            board.PrintBoard();

            for (int i = 0; i < shipCount; i++)
            {
                Console.WriteLine($"Ustaw swoje {shipName}");
                Console.WriteLine($"Ustaw {shipName} numer {i + 1}");

                BoardField[] fields = new BoardField[shipName.Length == 1 ? 1 : connectedCount];

                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = board.ReadField();
                }
                Ship ship = new Ship(fields, shipName, connectedCount);
                if (ship.CanBePlaced(board))
                {
                    foreach (var field in fields)
                    {
                        board.SetField(field, '#');
                    }
                    ships.Add(ship);
                } else
                {
                    Console.WriteLine("Nie można ustawić statku w taki sposób. Spróbuj ponownie.");
                    i--;
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Ustaw swoje statki");
                    board.PrintBoard();
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Ustaw swoje statki");
                board.PrintBoard();
            }
        }

        public void SetShips()
        {
            SetupShips("jednomasztowce", 4, 1);
            SetupShips("dwumasztowce", 3, 2);
            SetupShips("tzrymasztowce", 1, 3);
            SetupShips("czteromasztowce", 1, 4);
        }

        public void Shot()
        {
            Console.Clear();
            DisplayBoard();
            Console.WriteLine("Strzelaj");
            Console.WriteLine("Statki przeciwnika");
            BoardField shootedField = enemyBoard.ReadField();
            bool isShipHited = false;
            if(enemyBoard.GetField(shootedField) != '.')
            {
                Console.WriteLine("Ten punkt już został zestrzelony");
                Shot();
                return;
            } else
            {
                for (int i = enemyShips.Count - 1; i >= 0; i--)
                {
                    var ship = enemyShips[i];
                    foreach (var field in ship.GetFields())
                    {
                        if (field.IsMatching(shootedField))
                        {
                            Console.WriteLine("Trafiono!");
                            enemyBoard.SetField(field, 'X');
                            isShipHited = true;
                            if (ship.IsDestroyed())
                            {
                                Console.WriteLine("Statek zatopiony!");
                                enemyShips.Remove(ship);
                                foreach (var shipField in ship.GetFields())
                                {
                                    enemyBoard.SetField(shipField, '#');
                                    enemyBoard.MarkNeighboringFields(shipField);
                                }
                            }
                            break;
                        }
                    }
                    if (isShipHited)
                    {
                        if (IsWin())
                        {
                            wins++;
                            return;
                        }
                        else
                        {
                            Shot();
                            return;
                        }
                    }
                }
                if (!isShipHited)
                {
                    Console.WriteLine("Pudło!");
                    enemyBoard.SetField(shootedField, '*');
                }
            }
            Console.ReadKey();
        } 
        public int GetWins()
        {
            return wins;
        }

        public bool IsWin()
        {
            if (enemyShips.Count == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public void Restart()
        {
            board = new Board();
            enemyBoard = new Board();
            ships = new List<Ship>();
            enemyShips = new List<Ship>();
        }
    }
}
