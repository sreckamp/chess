using System.Collections.Generic;
using System.Drawing;

namespace Chess.Model.Evaluation.Models
{
    public interface IMarkingsProvider
    {
        void Mark<T>(Point point, T marker) where T : IMarker;

        void Mark<T>(Point point, IEnumerable<T> markers) where T : IMarker;

        IEnumerable<T> GetMarkers<T>(Point location) where T : IMarker;

        IEnumerable<T> GetMarkers<T>(Point location, MarkerType type) where T : IMarker;
    }
}