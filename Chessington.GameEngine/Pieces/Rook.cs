using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player, bool moved = false)
            : base(player, moved) { }

        public override IEnumerable<Square> GetPossibleMoves(Board board)
        {
            return GetLateralMoves(board);
        }
        public override int GetValue()
        {
            return 5;
        }

        public override Rook Copy()
        {
            return new Rook(Player, Moved);
        }
    }
}