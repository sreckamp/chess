using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Chess.Model.Extensions;

namespace Chess.Model.Models
{
    public class Board : IEnumerable<Board.Square>
    {
        private readonly Square[][] m_squares;
        public Board(int size, int corners)
        {
            Corners = corners;
            m_squares = new Square[size][];

            m_squares.TypedInitialize(size, (x, y) => new Square
            {
                X = x,
                Y = y,
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

        public Piece this[int x, int y]
        {
            set
            {
                if(!IsOnBoard(x, y)) throw new IndexOutOfRangeException();

                m_squares[y][x].Piece = value;
            }
            get
            {
                if(!IsOnBoard(x, y)) throw new IndexOutOfRangeException();

                return m_squares[y][x].Piece;
            }
        }

        public Board DeepCopy()
        {
            var board = new Board(Size, Corners);
            foreach (var square in this.Where(square => !square.IsEmpty))
            {
                board[square.X, square.Y] = square.Piece;
            }

            return board;
        }

        public void Update()
        {
            var sw = new Stopwatch();
            sw.Start();
            foreach (var square in this)
            {
                square.Update(this);
            }
            sw.Stop();
            Console.WriteLine($"Updated in {sw.ElapsedMilliseconds}mS");
        }

        public IEnumerable<Point> GetAvailable(Point location)
        {
            return GetAvailable(location.X, location.Y);
        }

        public IEnumerable<Point> GetAvailable(int x, int y)
        {
            return IsOnBoard(x, y) ? m_squares[y][x].Available : Enumerable.Empty<Point>();
        }

        public class Square
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Piece Piece { get; set; }

            public bool IsEmpty => Piece.IsEmpty;
            
            public IEnumerable<Point> Available { get; set; }

            public Dictionary<Direction, HashSet<Color>> AttackedBy { get; } = new Dictionary<Direction, HashSet<Color>>();

            public Direction PinDir { get; set; } = Direction.None;

            public void Update(Board board)
            {
                Available = !board.IsOnBoard(X,Y) || IsEmpty ? Enumerable.Empty<Point>() : GetAvailable(board);
            }

            private IEnumerable<Point> GetAvailable(Board board)
            {
                var location = new Point(X, Y);
                var result = new List<Point>();

                var testDirs = new List<Direction>();
                if (PinDir != Direction.None)
                {
                    testDirs.Add(PinDir);
                    testDirs.Add(PinDir.Opposite());
                }
                else
                {
                    testDirs.AddRange(Directions.All);
                }
                foreach (var direction in testDirs)
                {
                    var rule = Piece.MoveRules[direction];

                    for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                    {
                        var to = rule.GetResult(location, direction, d);

                        if(!board.IsOnBoard(to)) break;

                        var target= board[to.X, to.Y];

                        if (!target.IsEmpty) break;

                        result.Add(to);
                    }

                    rule = Piece.AttackRules[direction];
                    for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                    {
                        var to = rule.GetResult(location, direction, d);

                        if(!board.IsOnBoard(to)) break;

                        var target= board.m_squares[to.Y][to.X];

                        var attackDir = rule.MaxCount == 1 ? Direction.None : direction;

                        if (!target.AttackedBy.ContainsKey(attackDir))
                        {
                            target.AttackedBy[attackDir] = new HashSet<Color>();
                        }

                        target.AttackedBy[attackDir].Add(Piece.Color);

                        if (target.Piece.Color.Equals(Piece.Color)) break;

                        if (attackDir != Direction.None)
                        {
                            for (var pinPoint = to.Offset(attackDir, 1);
                                board.IsOnBoard(pinPoint);
                                pinPoint = pinPoint.Offset(attackDir, 1))
                            {
                                var pinTest = board[pinPoint.X, pinPoint.Y];

                                if (pinTest.IsEmpty) continue;

                                if (pinTest.Type != PieceType.King || pinTest.Color != target.Piece.Color) break;

                                target.PinDir = attackDir;
                                target.Update(board);
                                break;
                            }
                        }

                        if(result.Contains(to) || target.IsEmpty) continue;

                        result.Add(to);

                        break;
                    }
                }

                return result;
            }
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return new NestedArrayEnumerator<Square>(m_squares);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}