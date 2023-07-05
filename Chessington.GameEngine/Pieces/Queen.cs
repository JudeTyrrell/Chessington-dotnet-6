using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var lateral = GetLateralMoves(board);
            var diag = GetDiagonalMoves(board);
            return lateral.Concat(diag);
        }
    }
}