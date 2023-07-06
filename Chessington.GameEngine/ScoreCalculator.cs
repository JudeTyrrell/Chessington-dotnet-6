using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class ScoreCalculator
    {
        private IBoard _board;

        public ScoreCalculator(IBoard board)
        {
            _board = board;
        }

        public int GetWhiteScore()
        {
            // Should add up the value of all of the pieces that white has taken.
            var score = 0;
            foreach (Piece captured in _board.CapturedPieces)
            {
                if (captured.Player == Player.Black) {
                    score += captured.GetValue();
                }
            }
            return score;
        }

        public int GetBlackScore()
        {
            // Should add up the value of all of the pieces that black has taken.
            var score = 0;
            foreach (Piece captured in _board.CapturedPieces)
            {
                if (captured.Player == Player.White) {
                    score += captured.GetValue();
                }
            }
            return score;
        }
    }
}