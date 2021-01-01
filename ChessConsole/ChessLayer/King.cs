﻿using System;
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

            var position = new Position(Position.Row, Position.Column);

            // check N
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row - 1, position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check S
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row + 1, position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check NE
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row - 1, position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check SE
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row + 1, position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check E 
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row, position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check NW
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row - 1, position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check SW
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row + 1, position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check W
            position = new Position(base.Position.Row, base.Position.Column);
            position.SetValues(position.Row, position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }
            return possibleMovesMat;
        }
    }
}
