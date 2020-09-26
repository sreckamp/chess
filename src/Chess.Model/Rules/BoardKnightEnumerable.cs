using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class BoardKnightEnumerable : IEnumerable<Square>
    {
        private readonly GameBoard m_board;
        private readonly Point m_start;

        public BoardKnightEnumerable(GameBoard board, Point start)
        {
            m_board = board;
            m_start = start;
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return new BoardKnightEnumerator(m_board, m_start);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class BoardKnightEnumerator : IEnumerator<Square>
        {
            private static readonly Point[] Offsets = new[]
            {
                new Point(-1, 2), new Point(1, 2),
                new Point(2, 1), new Point(2, -1),
                new Point(1, -2), new Point(-1, -2),
                new Point(-2, -1), new Point(-2, 1)
            };

            private readonly GameBoard m_board;
            private readonly Point m_start;
            private int m_index = -1;

            public BoardKnightEnumerator(GameBoard board, Point start)
            {
                m_board = board;
                m_start = start;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                Point pt;
                do
                {
                    m_index++;
                    if (m_index == Offsets.Length) return false;
                    pt = m_start.CartesianOffset(Offsets[m_index]);
                } while (!m_board.IsOnBoard(pt));

                Current = m_board.GetSquare(pt);

                return true;
            }

            public void Reset()
            {
                m_index = -1;
            }

            public Square Current { get; private set; }

            object IEnumerator.Current => Current;
        }
    }
}