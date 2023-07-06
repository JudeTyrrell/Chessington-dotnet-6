using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player, bool moved = false) 
            : base(player, moved) { }

        public override IEnumerable<Square> GetPossibleMoves(Board board)
        {
            var position = board.FindPiece(this);

            var moves = new List<Square>();

            var sign = 1;
            
            if (Player == Player.White)
            {
                sign = -1;
            }
            
            if (!IsOccupiedOrOOB(board, position.Row + sign, position.Col))
            {
                moves.Add(new Square(position.Row + sign,position.Col));

                if (!IsOccupiedOrOOB(board, position.Row + (2 * sign), position.Col) && !Moved)
                {
                    moves.Add(new Square(position.Row + (2 * sign), position.Col));
                }
            }

            for (int i = -1; i < 2; i += 2)
            {
                if (IsOpposing(board, position.Row + sign, position.Col + i))
                {
                    moves.Add(new Square(position.Row + sign, position.Col + i));
                }

                if (board.LastMove != null && board.LastMove.Item1.Equals(Square.At(position.Row + 2 * sign, position.Col + i)) &&
                    board.LastMove.Item2.Equals(Square.At(position.Row, position.Col + i)))
                {
                    moves.Add(Square.At(position.Row + sign, position.Col + i));
                }
                
                
            }

            return moves;
        }
        
        
        public override int GetValue()
        {
            return 1;
        }
        
        public override Pawn Copy()
        {
            return new Pawn(Player, Moved);
        }
    }
}