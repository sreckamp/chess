using System.Collections.Generic;
using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface IPieceEnumerationProvider : IEnumerable<(Point,Piece)>
    {
        IEnumerable<(Point, Piece)> EnumerateStraightLine(Point start, Direction direction);

        IEnumerable<(Point, Piece)> EnumerateKnight(Point start);
    }

    public interface IBoard
    {
        Piece this[Point p] { get; set; }
    }
}
