using System.Collections.Generic;
using System.Linq;

namespace Chess.Model.Models
{
    /// <summary>
    /// Creates a board.
    /// Y is along file axis (0 = south, max = north)
    /// X is along rank axis (0 = west, max = east)
    /// </summary>
    public sealed class BoardFactory
    {
        private static readonly IEnumerable<Color> SKingFlips = new[] {Color.White, Color.Gold};

        private static class Lazy
        {
            public static readonly BoardFactory BoardFactory = new BoardFactory();
        }

        public static BoardFactory Instance => Lazy.BoardFactory;

        public GameBoard Create(Version version)
        {
            var template = Templates[version];

            var builder = new BoardBuilder()
                .SetSize(template.BoardSize)
                .SetCorners(template.CornerSize);

            foreach (var color in template.Colors.Where(color => color != Color.None))
                PopulateColor(builder, color, template.KingOnLeft);

            return builder.Build();
        }

        private static readonly PieceType[] SPower = new[]
        {
            PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen,
            PieceType.King, PieceType.Bishop, PieceType.Knight, PieceType.Rook
        };

        private static void PopulateColor(BoardBuilder builder, Color color, bool kingOnLeft)
        {
            for (var idx = 0; idx < SPower.Length; idx++)
            {
                var pieceIdx = kingOnLeft && SKingFlips.Contains(color) ? SPower.Length - 1 - idx : idx;
            
                builder.AddPieceRelativeToEdge(idx, 0, color, SPower[pieceIdx]);
                builder.AddPieceRelativeToEdge(idx, 1, color, PieceType.Pawn);
            }
        }

        private static readonly Dictionary<Version, Template> Templates = new Dictionary<Version, Template>
        {
            {Version.None, new Template
            {
                Colors = new[] {Color.None},
                BoardSize = 0,
                CornerSize = 0
            }},
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
