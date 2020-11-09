using System.Collections.Generic;
using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface IMarkingsProvider
    {
        void Mark<T>(Point point, T marker) where T : ISquareMarker;

        void Mark<T>(Point point, IEnumerable<T> markers) where T : ISquareMarker;

        IEnumerable<T> GetMarkers<T>(Point location) where T : ISquareMarker;

        IEnumerable<T> GetMarkers<T>(Point location, MarkerType type) where T : ISquareMarker;
    }
}