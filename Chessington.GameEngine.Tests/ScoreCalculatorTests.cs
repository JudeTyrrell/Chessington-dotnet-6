using System.Collections.Generic;
using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;
using FakeItEasy;

namespace Chessington.GameEngine.Tests
{
    [TestFixture]
    public class ScoreCalculatorTests
    {
        [Test]
        public void PawnGivesScoreOne()
        {
            var board = A.Fake<IBoard>();
            var capturedPieces = new List<Piece>();
            capturedPieces.Add(new Pawn(Player.Black));

            A.CallTo(() => board.CapturedPieces).Returns(capturedPieces);

            var calc = new ScoreCalculator(board);

            calc.GetWhiteScore().Should().Be(1);
        }
        
        [Test]
        public void BishopGivesScoreThree()
        {
            var board = A.Fake<IBoard>();
            var capturedPieces = new List<Piece>();
            capturedPieces.Add(new Bishop(Player.Black));

            A.CallTo(() => board.CapturedPieces).Returns(capturedPieces);

            var calc = new ScoreCalculator(board);

            calc.GetWhiteScore().Should().Be(3);
        }
    }
}