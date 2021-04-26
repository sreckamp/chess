using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;

namespace Chess.Model.Rules
{
    public sealed class StraightLineEnumerable<T> : IEnumerable<(Point, T)>
    {
        private readonly Point m_start;
        private readonly Direction m_direction;
        private readonly Func<Point, T> m_translator;
        private readonly Func<Point, bool> m_validator;

        public StraightLineEnumerable(Point start, Direction direction, Func<Point, T> translator,
            Func<Point, bool> validator)
        {
            m_start = start;
            m_direction = direction;
            m_translator = translator ?? (point => default);
            m_validator = validator ?? (point => true);
        }

        public IEnumerator<(Point, T)> GetEnumerator() =>
            new StraightLineEnumerator(m_start, m_direction, m_translator, m_validator);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class StraightLineEnumerator : IEnumerator<(Point, T)>
        {
            private readonly Point m_start;
            private readonly Direction m_direction;
            private readonly Func<Point, T> m_translator;
            private readonly Func<Point, bool> m_validator;
            private Point m_current;

            public StraightLineEnumerator(Point start, Direction direction, Func<Point, T> translator, Func<Point, bool> validator)
            {
                m_current = m_start = start;
                m_direction = direction;
                m_translator = translator;
                m_validator = validator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (m_direction == Direction.None) return false;

                m_current = m_current.PolarOffset(m_direction, 1);

                return m_validator(m_current);
            }

            public void Reset() => m_current = m_start;

            public (Point, T) Current => (m_current, m_translator(m_current));

            object IEnumerator.Current => Current;
        }
    }
}