using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {   
            var position = board.FindPiece(this);

            var moves = new List<Square>();

            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    if (Math.Abs(i) != Math.Abs(j) && i != 0 && j != 0)
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

                        // if (!IsOccupiedOrOOB(board, position.Row + i, position.Col + j))
                        // {
                        //     moves.Add(new Square(position.Row + i, position.Col + j));
                        // }
                        // if (board.GetPiece(new Square(position.Row + i, position.Col + j)).Player != Player)
                        // {
                        //     moves.Add(new Square(position.Row + i, position.Col + j));
                        // }
                    }
                }
            }
            return moves;
        }
    }
}