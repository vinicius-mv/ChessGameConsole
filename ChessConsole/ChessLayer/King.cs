using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;

namespace ChessConsole.ChessLayer
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        protected bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null || piece.Color != Color;
        }


        public override bool[,] PossibleMoves()
        {
            var mat = new bool[Board.Rows, Board.Columns];

            var pos = new Position(Position.Row, Position.Column);

            // check north
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check south
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check northeast
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check southeast
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check east 
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check northwest
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check southwest
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // check west
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}
