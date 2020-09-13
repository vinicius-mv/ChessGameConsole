

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

        public void PlacePiece(Piece piece, Position position)
        {
            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }
    }
}