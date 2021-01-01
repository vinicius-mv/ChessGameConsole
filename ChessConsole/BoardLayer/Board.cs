using System;
using System.CodeDom;

namespace ChessConsole.BoardLayer
{
    class Board
    {
        private Piece[,] _piecesBoardControl;

        public int Rows { get; set; }
        public int Columns { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _piecesBoardControl = new Piece[rows, columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return _piecesBoardControl[row, column];
        }

        public Piece GetPiece(Position position)
        {
            try
            {
                return _piecesBoardControl[position.Row, position.Column];
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new BoardException("Invalid position: out of board position.");
            }
        }

        public void PlacePiece(Piece piece, Position position)
        {
            _piecesBoardControl[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public bool IsThereAPiece(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        public Piece RemovePiece(Position position)
        {
            Piece piece = GetPiece(position);

            if (piece == null)
            {
                return null;
            }

            piece.Position = null;
            _piecesBoardControl[position.Row, position.Column] = null;

            return piece;
        }

        public bool IsValidPosition(Position position)
        {
            return position.Row < this.Rows && position.Row >= 0 && position.Column < this.Columns && position.Column >= 0;
        }

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
            {
                throw new BoardException("Invalid Position.");
            }
        }
    }
}