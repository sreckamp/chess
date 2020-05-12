using System.Drawing;

namespace Chess.Model
{
    public interface IMovementRule
    {
        int MinCount { get; }

        int MaxCount { get; }

        Point GetResult(Point start, Direction dir, int count);
    }
}