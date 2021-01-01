﻿using System;
using ChessConsole.BoardLayer;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace ChessConsole.ChessLayer
{
    internal class ChessMatch
    {

        public Board Board { get; private set; }
        public bool IsMatchCompleted { get; private set; }
        public int Turns { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool IsCheck { get; private set; }

        private readonly HashSet<Piece> _allPices;
        private HashSet<Piece> _capturedPieces;

        public ChessMatch()
        {
            _allPices = new HashSet<Piece>();
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

            // check if actual player didnt throw his own king in check condition
            if (GetCheckCondition(ActualPlayer))
            {
                RestoreMove(origin, destination, capturedPiece);
                throw new BoardException("Invalid move: can not let your own king in check condition.");
            }

            // check if the opponent has any possible move to escape check (checkmate)
            var opponentColor = GetOpponentColor(ActualPlayer);
            if (GetCheckCondition(opponentColor))
            {
                IsCheck = true;

                if (GetCheckMateCondition(opponentColor))
                {
                    IsMatchCompleted = true;
                    return;
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
            if(position == null)
            {
                throw new BoardException("Invalid position.");
            }

            if (Board.GetPiece(position) == null)
            {
                throw new BoardException("Invalid origin position: there is no piece at the position!");
            }

            if (ActualPlayer != Board.GetPiece(position).Color)
            {
                throw new BoardException("Invalid origin position: the piece selected is not yours!");
            }

            if (!Board.GetPiece(position).IsThereAnyPossibleMove())
            {
                throw new BoardException("Invalid origin position: the piece selected has no possible moves!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (destination == null)
            {
                throw new BoardException("Invalid position.");
            }

            if (!Board.GetPiece(origin).PossibleMove(destination))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        public IEnumerable<Piece> GetCapturedPieces(Color color)
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

        public Collection<Piece> GetPiecesInGame(Color color)
        {
            HashSet<Piece> piecesInGame = new HashSet<Piece>(_allPices);
            piecesInGame.ExceptWith(GetCapturedPieces(color));

            var finalListOfPices = new Collection<Piece>(piecesInGame.ToList());

            foreach (var piece in piecesInGame)
            {
                if (piece.Color != color)
                {
                    finalListOfPices.Remove(piece);
                }
            }

            return finalListOfPices;
        }

        private Color GetOpponentColor(Color color)
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
                throw new Exception($"Invalid game state: Thre is no {color} king in the board.");
            }

            var pieces = GetPiecesInGame(GetOpponentColor(color)); 


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
                            Position origin = new Position(piece.Position.Row, piece.Position.Column);
                            // check if any possible move could escape check
                            Piece capturedPiece = ExecuteMove(origin, destination);
                            bool isCheck = GetCheckCondition(color);
                            RestoreMove(origin, destination, capturedPiece);

                            if (!isCheck) // there is a move that could escape check
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
            _allPices.Add(piece);
        }

        private void PlacePiecesStartPosition()
        {
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));

            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));

            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));

            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('e', 1, new Queen(Board, Color.White));


            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));

            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));

            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));

            PlaceNewPiece('d', 8, new King(Board, Color.Black));
            PlaceNewPiece('e', 8, new Queen(Board, Color.Black));
        }
    }
}
