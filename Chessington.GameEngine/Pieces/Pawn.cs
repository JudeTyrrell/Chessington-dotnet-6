using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);

            var moves = new List<Square>();

            if (Player == Player.White)
            {
                if (!IsOccupied(board, position.Row - 1, position.Col))
                {
                    moves.Add(new Square(position.Row-1,position.Col));

                    if (!IsOccupied(board, position.Row - 2, position.Col) && !Moved)
                    {
                        moves.Add(new Square(position.Row-2, position.Col));
                    }
                }
            }
            
            else

            {
                if (!IsOccupied(board, position.Row + 1, position.Col))
                {
                    moves.Add(new Square(position.Row+1,position.Col));

                    if (!IsOccupied(board, position.Row + 2, position.Col) && !Moved)
                    {
                        moves.Add(new Square(position.Row +2 , position.Col));
                    }
                }
            }

            return moves;
        }
    }
}