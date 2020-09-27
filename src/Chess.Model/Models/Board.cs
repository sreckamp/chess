using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Extensions;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Rules;

namespace Chess.Model.Models
{
    public sealed class GameBoard : ISquareProvider
    {
        private readonly Square[][] m_squares;
        public GameBoard(int size, int corners)
        {
            Corners = corners;
            m_squares = new Square[size][];

            m_squares.TypedInitialize(size, (x, y) => new Square
            {
                Location = new Point(x, y),
                Edges = (!IsOnBoard(x - 1, y) ? new[] {Direction.West}
                        : !IsOnBoard(x + 1, y) ? new[] {Direction.East} : Enumerable.Empty<Direction>())
                    .Union(!IsOnBoard(x, y - 1) ? new[] {Direction.South}
                        : !IsOnBoard(x, y + 1) ? new[] {Direction.North} : Enumerable.Empty<Direction>()),
                Piece = Piece.CreateEmpty()
            });
        }

        public int Size => m_squares.Length;
        
        public int Corners { get; }

        public bool IsOnBoard(Point pt) => IsOnBoard(pt.X, pt.Y);

        public bool IsOnBoard(int x, int y) =>
            !((x < Corners || x >= Size - Corners) && (y < Corners || y >= Size - Corners))
            && (x >= 0 && x < Size)
            && (y >= 0 && y < Size);

        public Piece this[Point point]
        {
            set
            {
                if(!IsOnBoard(point)) throw new IndexOutOfRangeException();

                GetSquare(point).Piece = value;
            }
            get
            {
                if(!IsOnBoard(point)) throw new IndexOutOfRangeException();

                return GetSquare(point).Piece;
            }
        }

        public Piece this[int x, int y]
        {
            set => this[new Point(x, y)] = value;
            get => this[new Point(x, y)];
        }

        public Square GetSquare(Point p) => GetSquare(p.X,p.Y);

        public Square GetSquare(int x, int y) => m_squares[y][x];

        public GameBoard DeepCopy(Func<ISquareMarker, bool> keepMarker = null)
        {
            var board = new GameBoard(Size, Corners);
            foreach (var square in this.Where(square => !square.IsEmpty || square.GetMarkers<ISquareMarker>().Any()))
            {
                var newSquare = board.GetSquare(square.Location.X, square.Location.Y);

                newSquare.Piece = new Piece
                (
                    square.Piece.Type,
                    square.Piece.Color,
                    square.Piece.Edge
                );

                if (square.Piece.HasMoved)
                {
                    newSquare.Piece.Moved();
                }

                foreach (var marker in square.GetMarkers<ISquareMarker>().Where(keepMarker ?? (marker => true) ))
                {
                    newSquare.Mark(marker.Clone(board));
                }

                newSquare.Available = square.Available.Select(move => move.Clone());
                newSquare.Edges = square.Edges.ToList();
            }

            return board;
        }

        public IEnumerable<IMove> GetAvailable(Point location)
        {
            return GetAvailable(location.X, location.Y);
        }

        public IEnumerable<IMove> GetAvailable(int x, int y)
        {
            return IsOnBoard(x, y) ? GetSquare(x, y).Available : Enumerable.Empty<IMove>();
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return new NestedArrayEnumerator<Square>(m_squares, square => IsOnBoard(square.Location));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Square> EnumerateStraightLine(Point start, Direction direction)
            => new StraightLineEnumerable<Square>(start, direction, GetSquare, IsOnBoard);
        
        public IEnumerable<Square> EnumerateKnight(Point start)
            => new KnightOffsetEnumerable<Square>(start, GetSquare, IsOnBoard);
    }
}