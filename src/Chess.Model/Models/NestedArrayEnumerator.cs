using System;
using System.Collections;
using System.Collections.Generic;

namespace Chess.Model.Models
{
    public class NestedArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly T[][] m_array;
        private readonly Func<T, bool> m_validator;
        private int m_x = -1;
        private int m_y = 0;

        public NestedArrayEnumerator(T[][] array)
        : this(array, _ => true)
        {
        }

        public NestedArrayEnumerator(T[][] array, Func<T, bool> validator)
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
                
            } while (!m_validator(m_array[m_y][m_x]));

            return true;
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