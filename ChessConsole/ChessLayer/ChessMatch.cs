using System;
using ChessConsole.BoardLayer;
using System.Collections.Generic;
using System.Linq;

namespace ChessConsole.ChessLayer
{
    internal class ChessMatch
    {

        public Board Board { get; private set; }
        public bool IsMatchCompleted { get; private set; }
        public int Turns { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool IsCheck { get; private set; }

        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;

        public ChessMatch()
        {
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            Board = new Board(8, 8);

            Turns = 1;
            ActualPlayer = Color.White;
            IsMatchCompleted = false;
            IsCheck = false;

            PlacePiecesStartPosition();
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMove(origin, destination);

            if (GetCheckCondition(ActualPlayer))
            {
                RestoreMove(origin, destination, capturedPiece);
                throw new BoardException("Invalid move: your king is in check condition.");
            }

            if (GetCheckCondition(GetAdversaryColor(ActualPlayer)))
            {
                IsCheck = true;

                if (GetCheckMateCondition(GetAdversaryColor(ActualPlayer)))
                {
                    IsMatchCompleted = true;
                }
            }
            else
            {
                IsCheck = false;
            }


            Turns++;
            ChangePlayerTurn();
        }

        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementTotalMoves();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.PlacePiece(piece, destination);

            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
                _pieces.Remove(capturedPiece);
            }

            return capturedPiece;
        }

        private void RestoreMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecrementTotalMoves();

            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, destination);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.PlacePiece(piece, origin);
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

            if (!Board.GetPiece(position).IsThereAnyPossibleMove())
            {
                throw new Exception("Invalid origin position: the piece selected has no possible moves!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        public HashSet<Piece> GetCapturedPieces(Color color)
        {
            var capturedPiecesFromColor = new HashSet<Piece>();
            foreach (var piece in _capturedPieces)
            {
                if (piece.Color == color)
                {
                    capturedPiecesFromColor.Add(piece);
                }
            }
            return capturedPiecesFromColor;
        }

        public HashSet<Piece> GetPiecesInGame(Color color)
        {
            HashSet<Piece> piecesInGame = new HashSet<Piece>();

            foreach (var piece in _pieces)
            {
                if (piece.Color == color)
                {
                    piecesInGame.Add(piece);
                }
            }

            piecesInGame.ExceptWith(GetCapturedPieces(color));

            return piecesInGame;
        }

        private Color GetAdversaryColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

        private Piece GetKing(Color color)
        {
            foreach (var piece in GetPiecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool GetCheckCondition(Color color)
        {
            var king = GetKing(color);

            if (king == null)
            {
                throw new BoardException($"Invalid game state: Thre is no {color} king in the board.");
            }

            var pieces = GetPiecesInGame(GetAdversaryColor(color));


            foreach (var piece in pieces)
            {
                bool[,] possibleMovesMat = piece.PossibleMoves();

                if (possibleMovesMat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetCheckMateCondition(Color color)
        {
            foreach (Piece piece in GetPiecesInGame(color))
            {
                bool[,] possibleMovesMat = piece.PossibleMoves();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMovesMat[i, j])
                        {
                            Position destination = new Position(i, j);

                            // check if any possible move could escape check
                            Piece capturedPiece = ExecuteMove(piece.Position, destination);
                            bool testCheck = GetCheckCondition(color);
                            RestoreMove(piece.Position, destination, capturedPiece);

                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('d', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 1, new Rook(Board, Color.White));
            PlaceNewPiece('e', 2, new Rook(Board, Color.White));

            PlaceNewPiece('c', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('c', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
            PlaceNewPiece('e', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 8, new Rook(Board, Color.Black));
        }
    }
}
