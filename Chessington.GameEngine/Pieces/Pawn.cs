using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);

            var availableMoves = new List<Square>();

            Square newPosition;

            if (Player == Player.Black)
            {
                newPosition = new Square(position.Row + 1, position.Col);
            }
            else
            {
                newPosition = new Square(position.Row - 1, position.Col);
            }
            
            if (board.GetPiece(newPosition) == null)
            {
                availableMoves.Add(newPosition);
            }
            
            return availableMoves;
        }
    }
}