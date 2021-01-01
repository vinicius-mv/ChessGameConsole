using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;

namespace ChessConsole.ChessLayer
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "R";
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

            // check N 
            position.SetValues(base.Position.Row - 1, base.Position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }
                position.Row -= 1;
            }

            // check S 
            position.SetValues(base.Position.Row + 1, base.Position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Row += 1;
            }

            //check E 
            position.SetValues(base.Position.Row, base.Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Column += 1;
            }

            // check W 
            position.SetValues(base.Position.Row, base.Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Column -= 1;
            }

            return possibleMovesMat;
        }
    }
}
