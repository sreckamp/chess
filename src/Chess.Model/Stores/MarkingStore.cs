using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models.Board;

namespace Chess.Model.Stores
{
    public sealed class MarkingStore : IMarkingsProvider
    {
        private Dictionary<Point, List<IMarker>> m_markers
            = new Dictionary<Point, List<IMarker>>();

        public void Mark<T>(Point point, T marker) where T : IMarker
        {
            if (!m_markers.ContainsKey(point))
            {
                m_markers[point] = new List<IMarker>();
            }

            m_markers[point].Add(marker);
        }

        public void Mark<T>(Point point, IEnumerable<T> markers) where T : IMarker
        {
            foreach (var marker in markers)
            {
                Mark(point, marker);
            }
        }

        public IEnumerable<T> GetMarkers<T>(Point location) where T : IMarker =>
            m_markers.ContainsKey(location)
                ? m_markers[location].Where(marker => marker is T).Cast<T>()
                : Enumerable.Empty<T>();

        public IEnumerable<T> GetMarkers<T>(Point location, MarkerType type) where T : IMarker =>
            m_markers.ContainsKey(location)
                ? m_markers[location].Where(marker => marker is T && marker.Type == type).Cast<T>()
                : Enumerable.Empty<T>();

        public MarkingStore DeepClone() => Filter();

        public MarkingStore Filter(Func<IMarker, bool> predicate = null) => new MarkingStore
        {
            m_markers = m_markers.ToDictionary(pair => pair.Key,
                pair => pair.Value.Where(predicate ?? (_ => true)).Select(marker => marker.Clone()).ToList())
        };
    }
}