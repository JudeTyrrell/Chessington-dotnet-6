using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var position = board.FindPiece(this);

            var moves = new List<Square>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((i != 0 || j != 0))
                    {   
                        try
                        {
                            var piece = board.GetPiece(new Square(position.Row + i, position.Col + j));
                            if (piece==null || piece.Player != Player)
                            {
                                moves.Add(new Square(position.Row + i, position.Col + j));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return moves;
        }
    }
}