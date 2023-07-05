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
                if (board.GetPiece(new Square(i, position.Col)) != null)
                {
                    low = i + 1;
                }
            }
            for (int i = 7; i > position.Row; i--)
            {
                if (board.GetPiece(new Square(i, position.Col)) != null)
                {
                    high = i - 1;
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
                if (board.GetPiece(new Square(position.Row, i)) != null)
                {
                    low = i + 1;
                }
            }
            for (int i = 7; i > position.Col; i--)
            {
                if (board.GetPiece(new Square(i, position.Row)) != null)
                {
                    high = i - 1;
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
                x += 1;
                y -= 1; 
            }
            
            y = position.Col - 1;
            x = position.Row + 1; 
            
            while (!IsOccupied(board, x, y))
            {
                moves.Add(new Square(x,y ));
                x -= 1;
                y += 1; 
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

        public static bool IsOccupied(Board board, int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) return true;
            return board.GetPiece(new Square(x, y)) != null;
        }
    }
}