using System;
using System.Collections.Generic;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board : IBoard
    {
        private readonly Piece[,] _board;
        public Player CurrentPlayer { get; private set; }
        
        public IList<Piece> CapturedPieces { get; private set; }

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            _board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece piece)
        {
            _board[square.Row, square.Col] = piece;
        }
    
        public Piece GetPiece(Square square)
        {
            return _board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (_board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = _board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (_board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(_board[to.Row, to.Col]);
            }

            //Move the piece and set the 'from' square to be empty.
            _board[to.Row, to.Col] = _board[from.Row, from.Col];
            _board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }
        
        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            handler?.Invoke(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            handler?.Invoke(player);
        }

        public Board Copy(Player player)
        {
            var board = new Board(player);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = GetPiece(Square.At(x, y));
                    if (piece != null)
                    {
                        board.AddPiece(Square.At(x, y), piece.Copy());
                    }
                }
            }

            return board;
        }

        public bool InCheck(Player player)
        {
            foreach (var piece in _board)
            {
                if (piece != null)
                {
                    if (piece.Player != player)
                    {
                        foreach (var move in piece.GetPossibleMoves(this))
                        {
                            var capture = GetPiece(move);
                            if (capture != null && capture.GetType() == typeof(King) && capture.Player == player)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }
    }
}
