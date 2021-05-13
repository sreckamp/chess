using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Enumerables;
using Chess.Model.Evaluation.Models;
using Chess.Model.Extensions;

namespace Chess.Model.Models
{
    public sealed class GameBoard : IBoard, IPieceEnumerationProvider
    {
        private readonly Piece[][] m_pieces;
        public GameBoard(int size, int corners)
        {
            Corners = corners;
            m_pieces = new Piece[size][];

            m_pieces.TypedInitialize(size, (x, y) => Piece.Empty);
        }

        public IEnumerable<Direction> GetEdges(Point location) =>
            (!IsOnBoard(location.X - 1, location.Y) ? new[] {Direction.West}
                : !IsOnBoard(location.X + 1, location.Y) ? new[] {Direction.East} : Enumerable.Empty<Direction>())
            .Union(!IsOnBoard(location.X, location.Y - 1) ? new[] {Direction.South}
                : !IsOnBoard(location.X, location.Y + 1) ? new[] {Direction.North} : Enumerable.Empty<Direction>());

        public int Size => m_pieces.Length;
        
        public int Corners { get; }

        public bool IsOnBoard(Point pt) => IsOnBoard(pt.X, pt.Y);

        public bool IsOnBoard(int x, int y) =>
            !((x < Corners || x >= Size - Corners) && (y < Corners || y >= Size - Corners))
            && (x >= 0 && x < Size)
            && (y >= 0 && y < Size);

        public Piece this[Point point]
        {
            set => this[point.X, point.Y] = value;
            get => this[point.X, point.Y];
        }

        public Piece this[int x, int y]
        {
            set
            {
                if(!IsOnBoard(x, y)) throw new IndexOutOfRangeException();

                m_pieces[y][x] = value;
            }
            get
            {
                if(!IsOnBoard(x, y)) throw new IndexOutOfRangeException();

                return m_pieces[y][x];
            }
        }

        public GameBoard DeepCopy() => Filter();

        public GameBoard Filter(Func<Point, Piece, bool> predicate = null)
        {
            var board = new GameBoard(Size, Corners);
            foreach (var (point, piece) in this.Where((tuple) => !tuple.Item2.IsEmpty
                                           && (predicate == null || predicate(tuple.Item1, tuple.Item2)) ))
            {
                board[point] = piece;
            }

            return board;
        }

        public IEnumerator<(Point, Piece)> GetEnumerator() =>
            new NestedArrayEnumerator<Piece>(m_pieces, (x, y) => IsOnBoard(x, y));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<(Point, Piece)> EnumerateStraightLine(Point start, Direction direction)
            => new StraightLineEnumerable<Piece>(start, direction, 
                point => m_pieces[point.Y][point.X], IsOnBoard);
        
        public IEnumerable<(Point, Piece)> EnumerateKnight(Point start)
            => new KnightOffsetEnumerable<Piece>(start, 
                point => m_pieces[point.Y][point.X], IsOnBoard);
    }
}