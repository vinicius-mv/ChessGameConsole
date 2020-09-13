using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;

namespace ChessConsole.ChessLayer
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            Column = char.ToUpper(column);
            Row = row;
        }

        public Position ToPosition()
        {
            var position = new Position(0, 0);
            position.Row = 8 - Row;
            position.Column = Column - 'A';

            return position;
        }

        public override string ToString()
        {
            return "" + char.ToUpper(Column) + Row;
        }
    }
}
