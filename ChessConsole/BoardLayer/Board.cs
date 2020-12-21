using System;
using System.CodeDom;

namespace ChessConsole.BoardLayer
{
    class Board
    {
        private Piece[,] _pieces;

        public int Rows { get; set; }
        public int Columns { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _pieces = new Piece[rows, columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece GetPiece(Position position)
        {
            return _pieces[position.Row, position.Column];
        }

        public void PlacePiece(Piece piece, Position position)
        {
            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public bool IsThereAPiece(Position position) 
        {
            ValidatePosition(position); // throw exception if position is not valid
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
            _pieces[position.Row, position.Column] = null;

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