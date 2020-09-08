using System.Collections;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    public class NestedArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly T[][] m_array;
        private int m_x = -1;
        private int m_y = 0;

        public NestedArrayEnumerator(T[][] array)
        {
            m_array = array;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            m_x++;
            if (m_x < m_array[m_y].Length) return true;

            m_y++;
            m_x = 0;

            return m_y < m_array.Length;
        }

        public void Reset()
        {
            m_x = -1;
            m_y = 0;
        }

        public T Current => m_array[m_y][m_x];

        object IEnumerator.Current => Current;
    }
}