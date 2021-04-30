using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Models
{
    public interface IPieceEnumerationProvider : IEnumerable<(Point, Piece)>
    {
        IEnumerable<(Point, Piece)> EnumerateStraightLine(Point start, Direction direction);

        IEnumerable<(Point, Piece)> EnumerateKnight(Point start);
    }
}