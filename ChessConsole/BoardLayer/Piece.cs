namespace ChessConsole.BoardLayer
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

        public void IncrementMoves()
        {
            TotalMoves++;
        }

        protected bool CanMove(Position position)
        {
            Piece p = Board.GetPiece(position);

            return (p == null) || (p.Color != Color);
        }
    }
}