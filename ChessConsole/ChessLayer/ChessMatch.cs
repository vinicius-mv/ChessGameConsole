using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;

namespace ChessConsole.ChessLayer
{
    internal class ChessMatch
    {

        public Board Board { get; private set; }
        public bool IsFinished { get; private set; }
        public int Turns { get; private set; }
        public Color ActualPlayer { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turns = 1;
            ActualPlayer = Color.White;
            IsFinished = false;

            PlacePiecesStartPosition();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoves();

            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PlacePiece(piece, destination);
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turns++;

            ChangePlayerTurn();
        }

        private void ChangePlayerTurn()
        {
            int aux = 1 - (int)ActualPlayer;
            ActualPlayer = (Color)aux;
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.GetPiece(position) == null)
            {
                throw new BoardException("Invalid origin position: there is no piece at the position!");
            }

            if (ActualPlayer != Board.GetPiece(position).Color)
            {
                throw new Exception("Invalid origin position: the piece selected is not yours!");
            }

            if(!Board.GetPiece(position).IsThereAnyPossibleMove())
            {
                throw new Exception("Invalid origin position: the piece selected has no possible moves!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if(!Board.GetPiece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        private void PlacePiecesStartPosition()
        {
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PlacePiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
