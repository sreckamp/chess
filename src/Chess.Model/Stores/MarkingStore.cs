﻿using System;
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

        public ISet<Color> InCheck { get; private set; } = new HashSet<Color>();
        public IEnumerable<T> GetMarkers<T>(Point location) where T : IMarker =>
            m_markers.ContainsKey(location)
                ? m_markers[location].Where(marker => marker is T).Cast<T>()
                : Enumerable.Empty<T>();

        public IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker =>
            m_markers.ContainsKey(location)
                ? m_markers[location].Where(marker => marker is T && types.Contains(marker.Type)).Cast<T>()
                : Enumerable.Empty<T>();

        public MarkingStore DeepClone() => Filter();

        public MarkingStore Filter(Func<IMarker, bool> predicate = null) => new MarkingStore
        {
            InCheck = InCheck.ToArray().ToHashSet(),
            m_markers = m_markers.ToDictionary(pair => pair.Key,
                pair => pair.Value.Where(predicate ?? (_ => true)).Select(marker => marker.Clone()).ToList())
        };
    }
}