using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface IMarker
    {
        MarkerType Type { get; }
        Point Source { get; }
        IMarker Clone();
    }
}