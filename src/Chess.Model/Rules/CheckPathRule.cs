using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    /// <summary>
    /// Evaluates path for check
    /// </summary>
    public sealed class CheckPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public CheckPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start1, Path path, ISquareProvider squares)
        {
            if (!path.Moves.Any()) return;
            var start = squares.GetSquare(path.Moves.First().From);
            var points = path.Moves.Select(m => m.To).ToList();
            for (var i = 0; i < points.Count; i++)
            {
                var target = squares.GetSquare(points[i]);
                if (target.IsEmpty) continue;
                if (target.Piece.Color == start.Piece.Color ||
                    target.Piece.Type != PieceType.King) break;

                var marker = new CheckMarker(start, target, path.Direction);

                for (; i > -1; i--)
                {
                    squares.GetSquare(points[i]).Mark(marker);
                }

                start.Mark(marker);
                break;
            }

            m_chain.Apply(start, path, squares);
        }
    }
}