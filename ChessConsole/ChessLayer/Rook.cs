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

            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            var mat = new bool[Board.Rows, Board.Columns];

            // check north positions
            var position = new Position(Position.Row - 1, Position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }
                position.Row -= 1;
            }

            // check south positions
            position = new Position(Position.Row + 1, Position.Column);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Row += 1;
            }

            //check east positions
            position = new Position(Position.Row, Position.Column + 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Column += 1;
            }

            // check west positions
            position = new Position(Position.Row, Position.Column - 1);
            while (Board.IsValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;

                var auxPiece = Board.GetPiece(position);

                if (auxPiece != null && auxPiece.Color != Color)
                {
                    break;
                }

                position.Column -= 1;
            }

            return mat;
        }
    }
}
