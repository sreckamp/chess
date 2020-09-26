using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class BoardStraightLineEnumerable : IEnumerable<Square>
    {
        private readonly GameBoard m_board;
        private readonly Point m_start;
        private readonly Direction m_direction;
        private readonly bool m_includeStart;

        public BoardStraightLineEnumerable(GameBoard board, Point start, Direction direction, bool includeStart = false)
        {
            m_board = board;
            m_start = start;
            m_direction = direction;
            m_includeStart = includeStart;
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return new BoardStraightLineEnumerator(m_board, m_start, m_direction, m_includeStart);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class BoardStraightLineEnumerator : IEnumerator<Square>
        {
            private readonly GameBoard m_board;
            private readonly Point m_start;
            private readonly Direction m_direction;
            private bool m_first;
            private Point m_current;

            public BoardStraightLineEnumerator(GameBoard board, Point start, Direction direction, bool includeStart)
            {
                m_board = board;
                m_current = m_start = start;
                m_direction = direction;
                m_first = includeStart;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (m_direction == Direction.None && !m_first) return false;

                m_current = m_first ? m_current : m_current.PolarOffset(m_direction, 1);
                m_first = false;

                if (!m_board.IsOnBoard(m_current)) return false;

                Current = m_board.GetSquare(m_current);
                return true;
            }

            public void Reset()
            {
                m_first = true;
                m_current = m_start;
            }

            public Square Current { get; private set; }

            object IEnumerator.Current => Current;
        }
    }
}