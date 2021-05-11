using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Stores
{
    /// <summary>
    /// Current state of marking
    /// </summary>
    public sealed class MarkingStore : IMarkingsProvider
    {
        private Dictionary<Point, List<IMarker>> m_markers
            = new Dictionary<Point, List<IMarker>>();

        public IDictionary<Color, Point> KingLocations { get; private set; } = new Dictionary<Color, Point>();

        public IEnumerable<Color> AvailableColors { get; set; } = Enumerable.Empty<Color>();

        public void Mark<T>(Point point, params T[] markers) where T : IMarker
        {
            foreach (var marker in markers)
            {
                if (!m_markers.ContainsKey(point))
                {
                    m_markers[point] = new List<IMarker>();
                }

                m_markers[point].Add(marker);
            }
        }

        public IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker =>
            m_markers.ContainsKey(location)
                ? m_markers[location].Where(marker => marker is T
                                                      && (types == null || !types.Any() || types.Contains(marker.Type))).Cast<T>()
                : Enumerable.Empty<T>();

        public IEnumerable<T> GetKingMarkers<T>(Color color, params MarkerType[] types) where T : IMarker =>
            KingLocations.ContainsKey(color) ? GetMarkers<T>(KingLocations[color], types) : Enumerable.Empty<T>();

        public MarkingStore DeepClone() => Filter();

        public MarkingStore Filter(Func<IMarker, bool> predicate = null) => new MarkingStore
        {
            KingLocations = KingLocations.ToArray().ToDictionary(pair => pair.Key, pair => pair.Value),
            m_markers = m_markers.ToDictionary(pair => pair.Key,
                pair => pair.Value.Where(predicate ?? (_ => true)).Select(marker => marker.Clone()).ToList()),
            AvailableColors = AvailableColors.ToList()
        };
    }
}
