using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public bool Moved { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

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

            var y = position.Col + 1;
            var x = position.Row + 1;

            while (!IsOccupied(board, x, y))
            {
                moves.Add(new Square(x,y ));
                x += 1;
                y += 1; 
            }

            y = position.Col + 1;
            x = position.Row - 1; 
            
            while (!IsOccupied(board, x, y))
            {
                moves.Add(new Square(x,y ));
                y += 1;
                x -= 1; 
            }
            
            y = position.Col - 1;
            x = position.Row + 1; 
            
            while (!IsOccupied(board, x, y))
            {
                moves.Add(new Square(x,y ));
                y -= 1;
                x += 1; 
            }
            
            y = position.Col - 1;
            x = position.Row - 1;

            while (!IsOccupied(board, x, y))
            {
                moves.Add(new Square(x,y ));
                x -= 1;
                y -= 1; 
            }
            
            return moves;
        }

        public static bool IsOccupied(Board board, int row, int col)
        {
            if (row < 0 || row > 7 || col < 0 || col > 7) return true;
            return board.GetPiece(new Square(row, col)) != null;
        }
    }
}