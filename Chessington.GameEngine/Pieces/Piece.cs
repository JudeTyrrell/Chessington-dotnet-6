using System;
using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player, bool moved = false)
        {
            Player = player;
            Moved = moved;
        }

        public Player Player { get; private set; }

        public bool Moved { get; private set; }

        public abstract IEnumerable<Square> GetPossibleMoves(Board board);
        
        public IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);
            var moves = GetPossibleMoves(board);

            var noChecks = new List<Square>();
            
            foreach (var move in moves)
            {
                var nextBoard = board.Copy(Player);
                nextBoard.GetPiece(position).MoveTo(nextBoard, move);
                if (!nextBoard.InCheck(Player))
                {
                    noChecks.Add(move);
                }
            }

            return noChecks;
        }

        public abstract int GetValue();

        public void MoveTo(Board board, Square newSquare)
        {
            Moved = true;
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }

        public List<Square> GetLateralMoves(Board board)
        {
            var position = board.FindPiece(this);

            var moves = new List<Square>();

            var low = 0;
            var high = 7;
            
            for (int i = 0; i < position.Row; i++)
            {
                var piece = board.GetPiece(new Square(i, position.Col));
                if (piece != null)
                {
                    if (piece.Player == Player)
                    {
                        low = i + 1;
                    }
                    else
                    {
                        low = i;
                    }
                }
            }
            for (int i = 7; i > position.Row; i--)
            {
                var piece = board.GetPiece(new Square(i, position.Col));
                if (piece != null)
                {
                    if (piece.Player == Player)
                    {
                        high = i - 1;
                    }
                    else
                    {
                        high = i;
                    }
                }
            }

            for (int i = low; i <= high; i++)
            {
                moves.Add(new Square(i, position.Col));
            }
            
            low = 0;
            high = 7;
            
            for (int i = 0; i < position.Col; i++)
            {
                var piece = board.GetPiece(new Square(position.Row, i));
                if (piece != null)
                {
                    if (piece.Player == Player)
                    {
                        low = i + 1;
                    }
                    else
                    {
                        low = i;
                    }
                }
            }
            for (int i = 7; i > position.Col; i--)
            {
                var piece = board.GetPiece(new Square(position.Row, i));
                if (piece != null)
                {
                    if (piece.Player == Player)
                    {
                        high = i - 1;
                    }
                    else
                    {
                        high = i;
                    }
                }
            }

            for (int i = low; i <= high; i++)
            {
                moves.Add(new Square(position.Row, i));
            }

            moves.RemoveAll((square => square.Equals(position)));

            return moves;
        }

        public List<Square> GetDiagonalMoves(Board board)
        {
            var position = board.FindPiece(this);
            
            var moves = new List<Square>();

            for (var i = -1; i < 2; i += 2)
            {
                for (var j = -1; j < 2; j += 2)
                {
                    
                    var x = position.Row + i;
                    var y = position.Col + j;
                    
                    while (!IsOccupiedOrOOB(board, x, y))
                    {
                        moves.Add(new Square(x,y ));
                        x += i;
                        y += j; 
                    }

                    try
                    {
                        var piece = board.GetPiece(new Square(x,y));
                        if (piece.Player != Player)
                        {
                            moves.Add(new Square(x,y));
                        }
                    }
                    catch
                    {
                    }
                }
            }
            
            return moves;
        }

        public static bool IsOccupiedOrOOB(Board board, int row, int col)
        {
            if (row < 0 || row > 7 || col < 0 || col > 7) return true;
            return board.GetPiece(new Square(row, col)) != null;
        }
        
        public bool IsOpposing(Board board, int row, int col)
        { 
            if (row < 0 || row > 7 || col < 0 || col > 7)
            {
                return false;
            }
            
            var piece = board.GetPiece(new Square(row,col));
            if (piece == null)
            {
                return false;
            }
            return piece.Player != Player;
        }

        public abstract Piece Copy();
    }
}

