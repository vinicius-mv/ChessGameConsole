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
        private readonly ChessMatch _match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
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

        private bool TestRookCastiling(Position position)
        {
            if (!Board.IsValidPosition(position))
            {
                return false;
            }
            Piece piece = Board.GetPiece(position);

            return piece != null && piece is Rook && piece.Color == base.Color && piece.TotalMoves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            var possibleMovesMat = new bool[Board.Rows, Board.Columns];

            var position = new Position(0, 0);

            // check N
            position.SetValues(base.Position.Row - 1, base.Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check S
            position.SetValues(base.Position.Row + 1, base.Position.Column);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check NE
            position.SetValues(base.Position.Row - 1, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check SE
            position.SetValues(base.Position.Row + 1, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check E 
            position.SetValues(base.Position.Row, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check NW
            position.SetValues(base.Position.Row - 1, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check SW
            position.SetValues(base.Position.Row + 1, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check W
            position.SetValues(base.Position.Row, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && CanMove(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            #region Special Moves
            // check castling short
            Position positionRookRight = new Position(base.Position.Row, base.Position.Column + 3);
            if (TestRookCastiling(positionRookRight))
            {
                // position from king to 1 square right
                Position positionR1 = new Position(base.Position.Row, base.Position.Column + 1);
                // position from king to 2 squares right
                Position positionR2 = new Position(base.Position.Row, base.Position.Column + 2);

                if (Board.GetPiece(positionR1) == null && Board.GetPiece(positionR2) == null)
                {
                    possibleMovesMat[base.Position.Row, base.Position.Column + 2] = true;
                }
            }

            // check castling long
            Position positionRookLeft = new Position(base.Position.Row, base.Position.Column - 4);
            if (TestRookCastiling(positionRookLeft))
            {
                // position from king to 1 square left
                Position positionL1 = new Position(base.Position.Row, base.Position.Column - 1);
                // position from king to 2 squares left
                Position positionL2 = new Position(base.Position.Row, base.Position.Column - 2);
                // position from king to 3 squares left
                Position positionL3 = new Position(base.Position.Row, base.Position.Column - 3);
                if(Board.GetPiece(positionL1) == null && Board.GetPiece(positionL2) == null && Board.GetPiece(positionL3) == null)
                {
                    possibleMovesMat[base.Position.Row, base.Position.Column -2] = true;
                }
            }
            #endregion

            return possibleMovesMat;

        }
    }
}
