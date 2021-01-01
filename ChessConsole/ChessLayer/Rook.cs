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

            // check N 
            var position = new Position(Position.Row, Position.Column);
            position.SetValues(position.Row - 1, position.Column);
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
            position = new Position(Position.Row + 1, Position.Column);
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
            position = new Position(Position.Row, Position.Column + 1);
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
            position = new Position(Position.Row, Position.Column - 1);
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
