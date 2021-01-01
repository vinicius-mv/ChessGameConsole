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

        private bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null || piece.Color != base.Color;
        }


        public override bool[,] PossibleMoves()
        {
            var possibleMovesMat = new bool[Board.Rows, Board.Columns];

            var pos = new Position(Position.Row, Position.Column);

            // check N
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check S
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check NE
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check SE
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check E 
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row, pos.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check NW
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row - 1, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check SW
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row + 1, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }

            // check W
            pos = new Position(Position.Row, Position.Column);
            pos.SetValues(pos.Row, pos.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                possibleMovesMat[pos.Row, pos.Column] = true;
            }
            return possibleMovesMat;
        }
    }
}
