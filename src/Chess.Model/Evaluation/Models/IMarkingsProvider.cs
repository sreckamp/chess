using System.Collections.Generic;
using System.Drawing;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Evaluation.Models
{
    public interface IMarkingsProvider
    {
        IDictionary<Color ,Point> KingLocations { get; }

        void Mark<T>(Point point, params T[] markers) where T : IMarker;

        IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker;

        IEnumerable<T> GetKingMarkers<T>(Color color, params MarkerType[] types) where T : IMarker;
    }
}