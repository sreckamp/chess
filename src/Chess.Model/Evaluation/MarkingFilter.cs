using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Evaluation.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Evaluation
{
    public class MarkingFilter : IMarkingsProvider
    {
        private readonly Predicate<Point> m_predicate;
        private readonly IMarkingsProvider m_markings;

        public MarkingFilter(Predicate<Point> predicate, IMarkingsProvider markings)
        {
            m_predicate = predicate;
            m_markings = markings;
        }

        public IDictionary<Color, Point> KingLocations => m_markings.KingLocations;

        public void Mark<T>(Point point, params T[] markers) where T : IMarker
        {
            if (m_predicate.Invoke(point))
            {
                m_markings.Mark(point, markers);
            }
        }

        public IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker =>
            m_markings.GetMarkers<T>(location, types);

        public IEnumerable<T> GetKingMarkers<T>(Color color, params MarkerType[] types) where T : IMarker =>
            m_markings.GetKingMarkers<T>(color, types);
    }
}