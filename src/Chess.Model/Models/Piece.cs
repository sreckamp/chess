using System;

namespace Chess.Model.Models
{
    public class Piece
    {
        // private readonly Direction m_direction;
        // private readonly Dictionary<Direction, List<IMovementRule>> m_moveRules = new Dictionary<Direction, List<IMovementRule>>();

        public Piece(PieceType type, Color color, Direction edge)
        {
            Type = type;
            Color = color;
            Edge = edge;
        }

        public PieceType Type { get; }

        public Color Color { get; }

        public Direction Edge { get; }

        public bool IsEmpty => Type == PieceType.Empty;

        public bool HasMoved { get; private set; }

        public void Moved()
        {
            HasMoved = true;
        }

        /// <inheritdoc />
        public override string ToString() => IsEmpty ? string.Empty : $"{Color} {Type}";

        private static readonly Lazy<Piece> EmptyLazy = new Lazy<Piece>(() =>
        {
            var empty = new Piece(PieceType.Empty, Color.None, Direction.None);
        
            return empty;
        });

        public static Piece CreateEmpty() => EmptyLazy.Value;
    }
}