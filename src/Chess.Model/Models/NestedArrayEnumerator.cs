using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Chess.Model.Models
{
    public sealed class NestedArrayEnumerator<T> : IEnumerator<(Point, T)>
    {
        private readonly T[][] m_array;
        private readonly Func<int, int, bool> m_validator;
        private int m_x = -1;
        private int m_y;

        public NestedArrayEnumerator(T[][] array)
        : this(array, (x,y) => true)
        {
        }

        public NestedArrayEnumerator(T[][] array, Func<int, int, bool> validator)
        {
            m_array = array;
            m_validator = validator;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            do
            {
                m_x++;
                if (m_x != m_array[m_y].Length) continue;

                m_y++;
                m_x = 0;
                if (m_y == m_array.Length) return false;
                
            } while (!m_validator(m_x, m_y));

            return true;
        }

        public void Reset()
        {
            m_x = -1;
            m_y = 0;
        }

        public (Point, T) Current => (new Point(m_x, m_y), m_array[m_y][m_x]);

        object IEnumerator.Current => Current;
    }
}