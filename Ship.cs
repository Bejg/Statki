using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statki
{
    internal class Ship
    {
        private BoardField[] fields;
        private string shipName;
        private int fieldCount;
        private int lives;
        public Ship(BoardField[] fields, string shipName, int fieldCount)
        {
            this.fields = fields;
            this.shipName = shipName;
            this.fieldCount = fieldCount;
            lives = fieldCount;
        }

        public BoardField[] GetFields()
        {
            return fields;
        }

        private bool HasNeighboringShip(Board board)
        {
            foreach (var field in fields)
            {
                for (int i = (field.Row - 'A') - 1; i <= (field.Row - 'A') + 1; i++)
                {
                    for (int j = (field.Column - 1) - 1; j <= (field.Column - 1) + 1; j++)
                    {
                        if (i >= 0 && i < 10 && j >= 0 && j < 10 && !(i == (field.Row - 'A') && j == (field.Column - 1)))
                        {
                            if (board.GetField(new BoardField((char)('A' + i), j + 1)) == '#')
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool AreConnected()
        {
            if (fields.Length < 2) return true;
            for (int i = 0; i < fields.Length - 1; i++)
            {
                int row1 = fields[i].Row - 'A';
                int col1 = fields[i].Column - 1;
                int row2 = fields[i + 1].Row - 'A';
                int col2 = fields[i + 1].Column - 1;

                if (!((row1 == row2 && Math.Abs(col1 - col2) == 1) || (col1 == col2 && Math.Abs(row1 - row2) == 1)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanBePlaced(Board board)
        {
            if (!HasNeighboringShip(board) && AreConnected() && isFree(board))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDestroyed()
        {
            lives--;
            if(lives == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public bool isFree(Board board)
        {
            bool isFree = true;
            foreach(var field in fields)
            {
                if(board.GetField(field) != '.')
                {
                    isFree = false;
                    break;
                }
            }
            return isFree;
        }
    }
}
