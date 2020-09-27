using System.Linq;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class CoverPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public CoverPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start1, Path path, ISquareProvider squares)
        {
            if (!path.Moves.Any()) return;

            var start = squares.GetSquare(path.Moves.First().From);
            var points = path.Moves.Select(m => m.To).ToList();

            foreach (var target in points.Select(squares.GetSquare))
            {
                target.Mark(new SimpleMarker(MarkerType.Cover, start, path.Direction));

                if (!target.IsEmpty) break;
            }

            m_chain.Apply(start, path, squares);
        }
    }
}