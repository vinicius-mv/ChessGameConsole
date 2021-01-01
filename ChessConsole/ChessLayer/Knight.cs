using ChessConsole.BoardLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.ChessLayer
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null || piece.Color != base.Color;
        }

        public override bool[,] PossibleMoves()
        {
            var possibleMovesMat = new bool[Board.Rows, Board.Columns];
            var position = new Position(0, 0);

            // check row - 1 movemnts
            position.SetValues(base.Position.Row - 1, base.Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }
            position.SetValues(base.Position.Row - 1, base.Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check row + 1 movemnets
            position.SetValues(base.Position.Row + 1, base.Position.Column - 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }
            position.SetValues(base.Position.Row + 1, base.Position.Column + 2);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check row - 2 movements
            position.SetValues(base.Position.Row - 2, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }
            position.SetValues(base.Position.Row - 2, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check row + 2 movements
            position.SetValues(base.Position.Row + 2, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }
            position.SetValues(base.Position.Row + 2, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            return possibleMovesMat;
        }
    }
}
