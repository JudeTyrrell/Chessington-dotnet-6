using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player, bool moved = false)
            : base(player, moved) { }

        public override IEnumerable<Square> GetPossibleMoves(Board board)
        {
            var lateral = GetLateralMoves(board);
            var diag = GetDiagonalMoves(board);
            return lateral.Concat(diag);
        }
        public override int GetValue()
        {
            return 9;
        }
        
        public override Queen Copy()
        {
            return new Queen(Player, Moved);
        }
    }
}