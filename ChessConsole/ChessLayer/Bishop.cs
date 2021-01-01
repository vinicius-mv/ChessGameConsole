using ChessConsole.BoardLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.ChessLayer
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "B";
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

            // check NW
            position.SetValues(base.Position.Row - 1, base.Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
                position.SetValues(position.Row - 1, position.Column - 1);
            }

            // check NE
            position.SetValues(base.Position.Row - 1, base.Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
                position.SetValues(position.Row - 1, position.Column + 1);
            }

            // check SE
            position.SetValues(base.Position.Row + 1, base.Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
                position.SetValues(position.Row + 1, position.Column + 1);
            }

            // check SW
            position.SetValues(base.Position.Row + 1, base.Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
                position.SetValues(position.Row + 1, position.Column - 1);
            }

            return possibleMovesMat;
        }
    }
}
