using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    /// <summary>
    /// Rule used to detect and mark pins
    /// TODO: Unit tests
    /// </summary>
    public sealed class PinMarkPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public PinMarkPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (!path.Moves.Any()) return;

            var points = path.Moves.Select(m => m.To).ToList();

            var pinned = default(Square);

            foreach (var target in points.Select(squares.GetSquare).Where(target => !target.IsEmpty))
            {
                if (pinned == default)
                {
                    pinned = target;
                    continue;
                }

                if(target.Piece.Type != PieceType.King ||
                   target.Piece.Color != pinned.Piece.Color ||
                   start.Piece.Color == target.Piece.Color) break;

                pinned.Mark(new SimpleMarker(MarkerType.Pin, start, path.Direction));

                break;
            }

            m_chain.Apply(start, path, squares);
        }
    }
}