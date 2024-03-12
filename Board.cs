using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static statki.Program;

namespace statki
{
    internal class Board
    {
        private char[,] fields;

        public Board()
        {
            fields = new char[10, 10];
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    fields[i, j] = '.';
                }
            }
        }

        public BoardField ReadField()
        {
            Console.Write("Podaj pole: ");
            string coordinates = Console.ReadLine();
            string numField;
            char letterField;
            bool isNumbers = false;

            switch (coordinates.Length)
            {
                case 2:
                    numField = coordinates[1].ToString();
                    if (int.TryParse(coordinates[1].ToString(), out _))
                    {
                        isNumbers = true;
                    }
                    break;
                case 3:
                    numField = coordinates[1].ToString() + coordinates[2].ToString();
                    if (int.TryParse(coordinates[1].ToString(), out _) && int.TryParse(coordinates[2].ToString(), out _))
                    {
                        isNumbers = true;
                    }
                    break;
                default:
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    return ReadField();

            }

            if (char.IsLetter(coordinates[0]) && isNumbers)
            {
                letterField = char.ToUpper(coordinates[0]);
                if (letterField >= 'A' && letterField <= 'J' && int.Parse(numField) >= 1 && int.Parse(numField) <= 10)
                {
                    BoardField field = new BoardField(letterField, int.Parse(numField));
                    return field;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    return ReadField();
                }
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                return ReadField();
            }
        }
        public void MarkNeighboringFields(BoardField field)
        {
            for (int i = (field.Row - 'A') - 1; i <= (field.Row - 'A') + 1; i++)
            {
                for (int j = (field.Column - 1) - 1; j <= (field.Column - 1) + 1; j++)
                {
                    if (i >= 0 && i < 10 && j >= 0 && j < 10 && !(i == (field.Row - 'A') && j == (field.Column - 1)))
                    {
                        BoardField currentField = new BoardField((char)('A' + i), j + 1);
                        if (GetField(currentField) == '.')
                        {
                            SetField(currentField, '*');
                        }
                    }
                }
            }
        }

        public void SetField(BoardField field, char character)
        {
            
            int row = field.Row - 'A';
            int col = field.Column - 1;
            fields[row, col] = character;
        }

        public char GetField(BoardField field)
        {
            int row = field.Row - 'A';
            int col = field.Column - 1;
            return fields[row, col];
        }

        public void PrintBoard()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(fields[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
