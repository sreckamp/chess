using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface ISquareMarker
    {
        MarkerType Type { get; }
        Point Source { get; }
        // Direction Direction { get; }
        ISquareMarker Clone();
    }
}