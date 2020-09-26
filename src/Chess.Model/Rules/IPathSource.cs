using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public interface IPathSource
    {
        bool Applies(PieceType type);
        IEnumerable<Path> GetPaths(Square square, ISquareProvider squares);
    }
}