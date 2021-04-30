using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Extensions;

namespace Chess.Model.Evaluation.Enumerables
{
    public sealed class KnightOffsetEnumerable<T> : IEnumerable<(Point, T)>
    {
        private readonly Point m_start;
        private readonly Func<Point, T> m_translator;
        private readonly Func<Point, bool> m_validator;

        public KnightOffsetEnumerable(Point start, Func<Point, T> translator, Func<Point, bool> validator)
        {
            m_start = start;
            m_translator = translator ?? (point => default);
            m_validator = validator ?? (point => true);
        }

        public IEnumerator<(Point, T)> GetEnumerator() => new KnightOffsetEnumerator(m_start, m_translator, m_validator);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class KnightOffsetEnumerator : IEnumerator<(Point, T)>
        {
            // ReSharper disable once StaticMemberInGenericType
            private static readonly Point[] Offsets =
            {
                new Point(-1, 2), new Point(1, 2),
                new Point(2, 1), new Point(2, -1),
                new Point(1, -2), new Point(-1, -2),
                new Point(-2, -1), new Point(-2, 1)
            };

            private readonly Func<Point, T> m_translator;
            private readonly Func<Point, bool> m_validator;
            private readonly Point m_start;
            private int m_index = -1;
            private Point m_current;

            public KnightOffsetEnumerator(Point start, Func<Point, T> translator, Func<Point, bool> validator)
            {
                m_start = start;
                m_translator = translator;
                m_validator = validator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                do
                {
                    m_index++;
                    if (m_index == Offsets.Length) return false;
                    m_current = m_start.CartesianOffset(Offsets[m_index]);
                } while (!m_validator(m_current));

                return true;
            }

            public void Reset() => m_index = -1;

            public (Point, T) Current => (m_current, m_translator(m_current));

            object IEnumerator.Current => Current;
        }
    }
}