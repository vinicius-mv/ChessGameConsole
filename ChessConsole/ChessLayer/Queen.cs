using ChessConsole.BoardLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.ChessLayer
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece == null || piece.Color != base.Color;
        }

        public override bool[,] PossibleMoves()
        {
            var position = new Position(Position.Row, Position.Column);

            var rookAux = new Rook(Board, Color) { Position = position };
            var rookAuxPossibleMoves = rookAux.PossibleMoves();

            var bishopAux = new Bishop(Board, Color) { Position = position };
            var bishopAuxPossibleMoves = bishopAux.PossibleMoves();

            var possibleMovesMat = new bool[Board.Rows, Board.Columns];

            for(int i = 0; i < Board.Rows; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                    if(rookAuxPossibleMoves[i, j] || bishopAuxPossibleMoves[i,j])
                    {
                        possibleMovesMat[i, j] = true;
                    }
            }

            return possibleMovesMat;
        }
    }
}
