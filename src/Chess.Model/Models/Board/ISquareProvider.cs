using System.Collections.Generic;
using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface ISquareProvider : IEnumerable<Square>
    {
        Square GetSquare(Point p);

        Square GetSquare(int x, int y);

        IEnumerable<Square> EnumerateStraightLine(Point start, Direction direction);

        IEnumerable<Square> EnumerateKnight(Point start);
    }
}