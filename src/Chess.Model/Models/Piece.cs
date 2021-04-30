namespace Chess.Model.Models
{
    public struct Piece
    {
        public Piece(PieceType type, Color color, Direction edge)
        {
            Type = type;
            Color = color;
            Edge = edge;
            HasMoved = false;
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

        public static readonly Piece Empty = new Piece(PieceType.Empty, Color.None, Direction.None);
    }
}
