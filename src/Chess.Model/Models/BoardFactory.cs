using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Model.Extensions;
using Chess.Model.Stores;

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
            var board = new Board(template.BoardSize, template.CornerSize);
            foreach (var color in template.Colors)
            {
                PopulateColor(board, color, template.CornerSize, template.KingOnLeft);
            }

            board.Update();

            return new BoardStore
            {
                Board = board,
                Captured = m_templates[version].Colors.ToDictionary(color => color,
                    color => Enumerable.Empty<Piece>()),
            };
        }

        private static Direction[] SKingFlips = new[] {Direction.South, Direction.East};
        private static Direction[] SOpposite = new[] {Direction.North, Direction.East};
        private void PopulateColor(Board board, Color color, int cornerSize, bool kingOnLeft)
        {
            var startEdge = m_sides[color];

            var pieceFile = startEdge.IsMember(SOpposite) ? board.Size - 1 : 0;
            var pawnFile = startEdge.IsMember(SOpposite) ? board.Size - 2 : 1;
            var kingFile = startEdge.IsMember(SKingFlips) && kingOnLeft ? 3 : 4;

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

                var x = startEdge.IsNorthSouth() ? rank : pieceFile;
                var y = startEdge.IsNorthSouth() ? pieceFile : rank;

                board[x,y] = piece;

                x = startEdge.IsNorthSouth() ? rank : pawnFile;
                y = startEdge.IsNorthSouth() ? pawnFile : rank;

                board[x,y] = pawn;
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