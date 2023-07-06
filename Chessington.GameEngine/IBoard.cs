using System.Collections.Generic;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine;

public interface IBoard
{
    public IList<Piece> CapturedPieces { get; }
}