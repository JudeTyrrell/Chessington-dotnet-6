using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player, bool moved = false)
            : base(player, moved) { }

        public override IEnumerable<Square> GetPossibleMoves(Board board)
        {
            return GetDiagonalMoves(board);
        }

        public override int GetValue()
        {
            return 3;
        }
        
        public override Bishop Copy()
        {
            return new Bishop(Player, Moved);
        }
    }
}