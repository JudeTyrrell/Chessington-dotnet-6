using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player, bool moved = false)
            : base(player, moved) { }

        public override IEnumerable<Square> GetPossibleMoves(Board board)
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

                            if (!Moved)
                            {
                                int[] rooks = { 0, 7 };
                                foreach (int col in rooks)
                                {
                                    var rook = board.GetPiece(Square.At(position.Row, col));
                                    var direction = col == 0 ? -1 : 1;
                                    
                                    if (rook != null && !rook.Moved && rook.GetType() == typeof(Rook))
                                    {
                                        var nextBoard = board.Copy(Player);
                                        nextBoard.GetPiece(position).MoveTo(nextBoard, Square.At(position.Row, position.Col + direction));
                                        if (!nextBoard.InCheck(Player))
                                        {
                                            moves.Add(new Square(position.Row, position.Col + 2*direction));
                                        }
                                    }
                                }
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
        
        
        public override int GetValue()
        {
            return -1;
        }
        
        public override King Copy()
        {
            return new King(Player, Moved);
        }
    }
}