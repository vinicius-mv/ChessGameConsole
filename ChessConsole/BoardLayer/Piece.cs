﻿namespace ChessConsole.BoardLayer
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int TotalMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            TotalMoves = 0;
        }

        public abstract bool[,] PossibleMoves();

        public void IncrementTotalMoves()
        {
            TotalMoves++;
        }

        public void DecrementTotalMoves()
        {
            TotalMoves--;
        }

        public bool IsThereAnyPossibleMove()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMove(Position pos)
        {
            return PossibleMoves()[pos.Row, pos.Column];
        }
    }
}