using System.Collections.Generic;
using System.Drawing;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Evaluation.Models
{
    public interface IMarkingsProvider
    {
        ISet<Color> InCheck { get; }

        void Mark<T>(Point point, T marker) where T : IMarker;

        void Mark<T>(Point point, IEnumerable<T> markers) where T : IMarker;

        IEnumerable<T> GetMarkers<T>(Point location) where T : IMarker;

        IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker;
    }
}