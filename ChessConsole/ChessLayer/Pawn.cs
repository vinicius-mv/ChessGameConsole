using ChessConsole.BoardLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.ChessLayer
{
    internal class Pawn : Piece
    {
        private ChessMatch _match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsFree(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null;
        }

        private bool IsThereAnEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece != null && piece.Color != base.Color;
        }

        public override bool[,] PossibleMoves()
        {
            var possibleMovesMat = new bool[Board.Rows, Board.Columns];
            var position = new Position(0, 0);

            int signal = -1;   
            if (base.Color == Color.Black)
            {
                signal = +1;
            }

            // check simple move 'forward'
            position.SetValues(base.Position.Row + 1 * signal, base.Position.Column);
            if (Board.IsValidPosition(position) && IsFree(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check double move 'forward' (only first move)
            position.SetValues(base.Position.Row + 2 * signal, base.Position.Column);
            if (base.TotalMoves == 0 && Board.IsValidPosition(position) && IsFree(position) && possibleMovesMat[position.Row - 1 * signal, position.Column])
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check left diagonal move (attack opponent)
            position.SetValues(base.Position.Row + 1 * signal, base.Position.Column - 1);
            if (Board.IsValidPosition(position) && IsThereAnEnemy(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check right diagonal move (attack opponent)
            position.SetValues(base.Position.Row + 1 * signal, base.Position.Column + 1);
            if (Board.IsValidPosition(position) && IsThereAnEnemy(position))
            {
                possibleMovesMat[position.Row, position.Column] = true;
            }

            // check en passant
            position.SetValues(base.Position.Row, base.Position.Column);
            if (position.Row == 3 || position.Row == 4)
            {
                Position leftPosition = new Position(position.Row, position.Column - 1);
                if (Board.IsValidPosition(leftPosition) && IsThereAnEnemy(leftPosition) && Board.GetPiece(leftPosition) == _match.EnPassantVulnerable)
                {
                    possibleMovesMat[leftPosition.Row + 1 * signal, leftPosition.Column] = true;
                }

                Position rightPosition = new Position(position.Row, position.Column + 1);
                if (Board.IsValidPosition(rightPosition) && IsThereAnEnemy(rightPosition) && Board.GetPiece(rightPosition) == _match.EnPassantVulnerable)
                {
                    possibleMovesMat[rightPosition.Row + 1 * signal, rightPosition.Column] = true;
                }
            }
            return possibleMovesMat;
        }
    }

}
