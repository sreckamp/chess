using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Model.Extensions;

namespace Chess.Model.Models
{
    // TODO: convert to array
    // TODO: Create "square" class that has attacked by field (color & rule) & move to.  And some sort of pinning detection.  Only need to evaluate straight paths > 1.
    // TODO: Pin rule.  If attacked by another color with straight > 1 attack rule.  If continuing direction is king, can only move in that direction or opposite.
    // TODO: Castle Rule.  Look east or west or north or south.  If none of the squares are under any attack && next square is rook && king && rook have not moved, castling is allowed. (indicate 2 squares to side of king)
    // TODO: Check king is under attack (check)
    /// <summary>
    /// Creates a board.
    /// Y is along file axis (0 = south, max = north)
    /// X is along rank axis (0 = west, max = east)
    /// </summary>
    public class BoardFactory
    {
        private static class Lazy
        {
            public static readonly BoardFactory BoardFactory = new BoardFactory();
        }

        public static BoardFactory Instance => Lazy.BoardFactory;

        public BoardStore Create(Version version)
        {
            var template = m_templates[version];
            var board = new Piece[template.BoardSize][].TypedInitialize(template.BoardSize, Piece.CreateEmpty());
            foreach (var color in template.Colors)
            {
                PopulateColor(board, color, template.CornerSize, template.KingOnLeft);
            }

            return new BoardStore
            {
                Size = m_templates[version].BoardSize,
                CornerSize = m_templates[version].CornerSize,
                Board = board,
                Captured = m_templates[version].Colors.ToDictionary(color => color,
                    color => Enumerable.Empty<Piece>()),
            };
        }

        private void PopulateColor(Piece[][] board, Color color, int cornerSize, bool kingOnLeft)
        {
            var startEdge = m_sides[color];
            var northSouth = true;
            var pieceFile = 0;
            var pawnFile = 1;
            var kingFile = 4;

            switch (startEdge)
            {
                case Direction.South:
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.West:
                    northSouth = false;
                    break;
                case Direction.North:
                    pieceFile = board.Length - 1;
                    pawnFile = board.Length - 2;
                    break;
                case Direction.East:
                    northSouth = false;

                    pieceFile = board[0].Length - 1;
                    pawnFile = board[0].Length - 2;
                    kingFile = kingOnLeft ? 3 : kingFile;
                    break;
                case Direction.NorthEast:
                case Direction.SouthEast:
                case Direction.SouthWest:
                case Direction.NorthWest:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var idx = 0; idx < 8; idx++)
            {
                var pawn = Piece.CreatePawn(color, startEdge.Opposite());
                Piece piece;

                switch (idx)
                {
                    case 0:
                    case 7:
                        piece = Piece.CreateRook(color);
                        break;
                    case 1:
                    case 6:
                        piece = Piece.CreateKnight(color);
                        break;
                    case 2:
                    case 5:
                        piece = Piece.CreateBishop(color);
                        break;
                    default:
                        piece = idx == kingFile
                            ? Piece.CreateKing(color)
                            : Piece.CreateQueen(color);
                        break;
                }

                var rank = idx + cornerSize;

                var x = northSouth ? rank : pieceFile;
                var y = northSouth ? pieceFile : rank;

                board[y][x] = piece;

                x = northSouth ? rank : pawnFile;
                y = northSouth ? pawnFile : rank;

                board[y][x] = pawn;
            }
        }

        private readonly Dictionary<Color, Direction> m_sides = new Dictionary<Color, Direction>
        {
            {Color.White, Direction.South},
            {Color.Black, Direction.North},
            {Color.Silver, Direction.West},
            {Color.Gold, Direction.East},
        };

        private readonly Dictionary<Version, Template> m_templates = new Dictionary<Version, Template>
        {
            {Version.TwoPlayer, new Template
            {
                Colors = new[] {Color.White, Color.Black},
                BoardSize = 8,
                CornerSize = 0
            }},
            {Version.FourPlayer,new Template
                {
                    Colors = new [] {Color.White, Color.Silver, Color.Black, Color.Gold},
                    BoardSize = 14,
                    CornerSize = 3
                }
            }
        };

        private class Template
        {
            public IEnumerable<Color> Colors;
            public int CornerSize;
            public int BoardSize;
            public bool KingOnLeft => CornerSize > 0;
        }
    }
}