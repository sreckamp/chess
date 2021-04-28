using System;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Evaluation.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Evaluation
{
    public class MarkingFilter : IMarkingsProvider
    {
        private readonly Predicate<(IMarkingsProvider, Point)> m_predicate;
        private readonly IMarkingsProvider m_markings;

        public MarkingFilter(Predicate<(IMarkingsProvider, Point)> predicate, IMarkingsProvider markings)
        {
            m_predicate = predicate;
            m_markings = markings;
        }

        public ISet<Color> InCheck => m_markings.InCheck;
        public void Mark<T>(Point point, T marker) where T : IMarker
        {
            if (m_predicate.Invoke((this, point)))
            {
                m_markings.Mark(point, marker);
            }
        }

        public void Mark<T>(Point point, IEnumerable<T> markers) where T : IMarker
        {
            if (m_predicate.Invoke((this, point)))
            {
                m_markings.Mark(point, markers);
            }
        }

        public IEnumerable<T> GetMarkers<T>(Point location) where T : IMarker =>
            m_markings.GetMarkers<T>(location);

        public IEnumerable<T> GetMarkers<T>(Point location, params MarkerType[] types) where T : IMarker =>
            m_markings.GetMarkers<T>(location, types);
    }
}