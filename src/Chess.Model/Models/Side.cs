using System.Collections.Generic;
using System.Linq;

namespace Chess.Model.Models
{
    public sealed class Side
    {
        public static readonly Side Empty = new Side {Color = Color.None, Edge = Direction.None};
        public Color Color { get; set; }
        public Direction Edge { get; set; }
        public IEnumerable<Piece> Captured { get; set; } = Enumerable.Empty<Piece>();
    }
}
