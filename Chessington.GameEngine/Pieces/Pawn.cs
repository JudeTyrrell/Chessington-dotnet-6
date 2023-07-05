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

            var newPositions = new List<Square>();

            if (Player == Player.Black)
            {
                newPositions.Add(new Square(position.Row + 1, position.Col));

                if (!Moved)
                {
                    newPositions.Add(new Square(position.Row + 2, position.Col));
                }
            }
            else
            {
                newPositions.Add(new Square(position.Row - 1, position.Col));
                
                if (!Moved)
                {
                    newPositions.Add(new Square(position.Row - 2, position.Col));
                }
                
            }
            
            foreach (Square newPosition in newPositions)
            {
                if (board.GetPiece(newPosition) == null)
                {
                    availableMoves.Add(newPosition);
                }
            }
            
            
            return availableMoves;
        }
    }
}