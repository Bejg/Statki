using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statki
{
    internal class BoardField
    {
        public int Column { get; private set; }
        public char Row { get; private set; }

        public BoardField(char column, int row)
        {
                Column = row;
                Row = column;
        }

        public bool IsMatching(BoardField field)
        {
            if (Column == field.Column && Row == field.Row)
            {
                return true;
            } else
            {
                return false;
            }
        }

    }
}
