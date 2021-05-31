using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Extensions;

namespace Chess.Model.Models
{
    public sealed class BoardBuilder
    {
        private static readonly IEnumerable<Direction> SOpposite = new[] {Direction.North, Direction.East};

        private static readonly Dictionary<Color, Direction> SDirectionForColor = new Dictionary<Color, Direction>
        {
            {Color.None, Direction.None},
            {Color.White, Direction.South},
            {Color.Black, Direction.North},
            {Color.Silver, Direction.West},
            {Color.Gold, Direction.East}
        };

        private int m_size = 8;
        private int m_corners;

        private readonly IDictionary<Point, Piece> m_placements = new Dictionary<Point, Piece>();

        public BoardBuilder SetCorners(int value)
        {
            m_corners = value;

            return this;
        }

        public BoardBuilder SetSize(int value)
        {
            m_size = value;

            return this;
        }

        public BoardBuilder AddPieceRelativeToEdge(int relativeX, int relativeY, Color color, PieceType type)
        {
            var edge = SDirectionForColor[color];

            var rank = edge.IsMember(SOpposite) ? m_size - relativeY - 1 : relativeY;
            var file = m_corners + relativeX;

            var x = edge.IsNorthSouth() ? file : rank;
            var y = edge.IsNorthSouth() ? rank : file;

            m_placements[new Point(x, y)] = new Piece(type, color, SDirectionForColor[color]);
            
            return this;
        }

        public BoardBuilder AddPiece(int x, int y, Color color, PieceType type, bool hasMoved = false)
        {
            m_placements[new Point(x, y)] = CreatePiece(color, type, hasMoved);
            
            return this;
        }

        public Piece CreatePiece(Color color, PieceType type, bool hasMoved = false)
        {
            var piece = new Piece(type, color, SDirectionForColor[color]);
            if (hasMoved)
            {
                piece.Moved();
            }

            return piece;
        }

        public GameBoard Build()
        {
            var board = new GameBoard(m_size, m_corners);

            foreach (var (point, piece) in m_placements)
            {
                board[point.X, point.Y] = piece;
            }

            return board;
        }
    }
}