using System.Collections.Generic;
using Chessington.GameEngine.Pieces;
using NUnit.Framework;
using FluentAssertions;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class CheckTests
    {
        [Test]
        public void WhiteKingCantMoveIntoCheck()
        {
            
                var board = new Board();
                var king = new King(Player.White);
                var rook = new Rook(Player.Black);
                board.AddPiece(Square.At(4, 4), king);
                board.AddPiece(Square.At(3,3), rook );

                var moves = king.GetAvailableMoves(board);

                var expectedMoves = new List<Square>
                {
                    Square.At(5, 5),
                    Square.At(5, 4),
                    Square.At(4, 5),
                    Square.At(3, 3)
                };

                moves.Should().BeEquivalentTo(expectedMoves);
            }
        
        [Test]
        public void PawnCantMoveIntoCheck()
        {
            
            var board = new Board();
            var king = new King(Player.White);
            var rook = new Rook(Player.Black);
            var pawn = new Pawn(Player.White);
            
            board.AddPiece(Square.At(4, 4), king);
            board.AddPiece(Square.At(2,4), rook);
            board.AddPiece(Square.At(3,4),pawn);

            var moves = pawn.GetAvailableMoves(board);

            moves.Should().BeEquivalentTo(new List<Square>());
        }
        
        [Test]
        public void BishopCantMoveIntoCheckDespitePin()
        {
            var board = new Board();
            var wking = new King(Player.White);
            var bking = new King(Player.Black);
            var rook = new Rook(Player.White);
            var wbishop = new Bishop(Player.White);
            var bishop = new Bishop(Player.Black);
            
            board.AddPiece(Square.At(0, 0), wking);
            board.AddPiece(Square.At(1, 1), wbishop);
            board.AddPiece(Square.At(0, 2), rook);
            board.AddPiece(Square.At(3, 2), bking);
            board.AddPiece(Square.At(2, 2), bishop);

            var moves = wbishop.GetAvailableMoves(board);

            moves.Should().BeEquivalentTo(new List<Square>
            {
                Square.At(2,2)
            });
        }
    }

}
