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

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Position.Row, Position.Column];

            Position position = new Position(0, 0);

            // check north positions
            position.SetValuesPosition(position.Row - 1, position.Column);
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
            position.SetValuesPosition(position.Row + 1, position.Column);
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
            position.SetValuesPosition(position.Row, position.Column + 1);
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
            position.SetValuesPosition(position.Row, position.Column - 1);
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
