using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameBase.Model;

namespace Chess.Model.Models
{
    public class BoardFactory
    {
        private static class Lazy
        {
            public static readonly BoardFactory BoardFactory = new BoardFactory();
        }

        public static BoardFactory Instance => Lazy.BoardFactory;

        public BoardStore Create(Version version)
        {
            return new BoardStore
            {
                Size = m_templates[version].BoardSize,
                CornerSize = m_templates[version].CornerSize,
                Placements = m_templates[version].Colors.SelectMany(
                    color => PopulateFromTemplate(m_templates[version], color),
                    (color, placement) => placement),
                Captured = m_templates[version].Colors.ToDictionary(color => color,
                    color => Enumerable.Empty<Piece>()),
            };
        }

        private IEnumerable<Placement<Piece>> PopulateFromTemplate(Template template, Color color)
        {
            var dir = m_sides[color];
            var northSouth = true;
            var pieceFile = 0;
            var pawnFile = 1;
            var kingFile = 4;

            var placements = new List<Placement<Piece>>();

            switch (dir)
            {
                case Direction.South:
                    pieceFile = template.BoardSize - 1;
                    pawnFile = template.BoardSize - 2;
                    kingFile = template.KingOnLeft ? 3 : kingFile;
                    break;
                case Direction.West:
                    northSouth = false;
                    kingFile = template.KingOnLeft ? 3 : kingFile;
                    break;
                case Direction.North:
                    break;
                case Direction.East:
                    pieceFile = template.BoardSize - 1;
                    pawnFile = template.BoardSize - 2;
                    northSouth = false;
                    break;
                case Direction.NorthEast:
                case Direction.SouthEast:
                case Direction.SouthWest:
                case Direction.NorthWest:
                    return Enumerable.Empty<Placement<Piece>>();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var idx = 0; idx < 8; idx++)
            {
                var pawn = Piece.CreatePawn(color, dir.Opposite());
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

                var rank = idx + template.CornerSize;

                var powerPnt = northSouth
                    ? new Point(rank, pieceFile)
                    : new Point(pieceFile, rank);

                placements.Add(new Placement<Piece>(piece, powerPnt));

                var pawnPnt = northSouth
                    ? new Point(rank, pawnFile)
                    : new Point(pawnFile, rank);

                placements.Add(new Placement<Piece>(pawn, pawnPnt));
            }

            return placements;
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
                CornerSize = 0,
                KingOnLeft = false
            }},
            {Version.FourPlayer,new Template
                {
                    Colors = new [] {Color.White, Color.Silver, Color.Black, Color.Gold},
                    BoardSize = 14,
                    CornerSize = 3,
                    KingOnLeft = true
                }
            }
        };

        private class Template
        {
            public IEnumerable<Color> Colors;
            public int CornerSize;
            public int BoardSize;
            public bool KingOnLeft;
        }
    }
}