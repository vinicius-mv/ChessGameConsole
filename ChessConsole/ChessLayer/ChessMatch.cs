using System;
using ChessConsole.BoardLayer;
using System.Collections.Generic;

namespace ChessConsole.ChessLayer
{
    internal class ChessMatch
    {
        
        public Board Board { get; private set; }
        public bool IsFinished { get; private set; }
        public int Turns { get; private set; }
        public Color ActualPlayer { get; private set; }

        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;

        public ChessMatch()
        {
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            Board = new Board(8, 8);

            Turns = 1;
            ActualPlayer = Color.White;
            IsFinished = false;

            PlacePiecesStartPosition();
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turns++;

            ChangePlayerTurn();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PlacePiece(piece, destination);

            if(capturedPiece != null)
            {
               _capturedPieces.Add(capturedPiece); 
            }
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

        public HashSet<Piece> GetCapturedPieces(Color color)
        {
            var capturedPiecesFromColor = new HashSet<Piece>();
            foreach (var piece in _capturedPieces)
            {
                if(piece.Color == color)
                {
                    capturedPiecesFromColor.Add(piece);
                }
            }
            return capturedPiecesFromColor;
        }

        public HashSet<Piece> GetPiecesInGame(Color color)
        {
            HashSet<Piece> piecesInGame = new HashSet<Piece>(_pieces);

            foreach (var piece in piecesInGame)
            {
                piecesInGame.Add(piece);
            }
            piecesInGame.ExceptWith(GetCapturedPieces(color));

            return piecesInGame;
        }

        public void PlaceNewPiece(char column, int row, Piece piece) 
        {
            Board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void PlacePiecesStartPosition()
        {
            PlaceNewPiece('c', 1, new Rook(Board, Color.White));
            PlaceNewPiece('c', 2, new Rook(Board, Color.White));
            PlaceNewPiece('d', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 1, new Rook(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));

            PlaceNewPiece('c', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('c', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
