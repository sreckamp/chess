using System.Collections.Generic;
using System.Linq;

namespace Chess.Model.Models
{
    /// <summary>
    /// Creates a board.
    /// Y is along file axis (0 = south, max = north)
    /// X is along rank axis (0 = west, max = east)
    /// </summary>
    public sealed class BoardStoreFactory
    {
        private static class Lazy
        {
            public static readonly BoardStoreFactory BoardFactory = new BoardStoreFactory();
        }

        public static BoardStoreFactory Instance => Lazy.BoardFactory;

        public GameBoard Create(Version version)
        {
            var template = m_templates[version];
            var board = new GameBoard(template.BoardSize, template.CornerSize);
            foreach (var color in template.Colors)
                PopulateColor(board, color, template.CornerSize, template.KingOnLeft);

            return board;
        }

        private static readonly Direction[] SKingFlips = new[] {Direction.South, Direction.East};
        private static readonly Direction[] SOpposite = new[] {Direction.North, Direction.East};
        private void PopulateColor(GameBoard board, Color color, int cornerSize, bool kingOnLeft)
        {
            var side = m_sides.First(s => s.Color == color);

            var pieceFile = side.Edge.IsMember(SOpposite) ? board.Size - 1 : 0;
            var pawnFile = side.Edge.IsMember(SOpposite) ? board.Size - 2 : 1;
            var kingFile = side.Edge.IsMember(SKingFlips) && kingOnLeft ? 3 : 4;

            for (var idx = 0; idx < 8; idx++)
            {
                var pawn = new Piece(PieceType.Pawn, color, side.Edge);
                Piece piece;
            
                switch (idx)
                {
                    case 0:
                    case 7:
                        piece = new Piece(PieceType.Rook, color, side.Edge);
                        break;
                    case 1:
                    case 6:
                        piece = new Piece(PieceType.Knight, color, side.Edge);
                        break;
                    case 2:
                    case 5:
                        piece = new Piece(PieceType.Bishop, color, side.Edge);
                        break;
                    default:
                        piece = new Piece(idx == kingFile ? PieceType.King : PieceType.Queen, color, side.Edge);
                        break;
                }
            
                var rank = idx + cornerSize;
            
                var x = side.Edge.IsNorthSouth() ? rank : pieceFile;
                var y = side.Edge.IsNorthSouth() ? pieceFile : rank;
            
                board[x,y] = piece;
            
                x = side.Edge.IsNorthSouth() ? rank : pawnFile;
                y = side.Edge.IsNorthSouth() ? pawnFile : rank;
            
                board[x,y] = pawn;
            }
        }

        private readonly IEnumerable<Side> m_sides = new List<Side>
        {
            new Side{ Color = Color.White, Edge = Direction.South},
            new Side{ Color = Color.Black, Edge = Direction.North},
            new Side{ Color = Color.Silver, Edge = Direction.West},
            new Side{ Color = Color.Gold, Edge = Direction.East},
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