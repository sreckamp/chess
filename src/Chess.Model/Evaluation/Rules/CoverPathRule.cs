using System.Linq;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

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
        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                foreach (var (target, piece) in path.Squares)
                {
                    markings.Mark(target, new SimpleMarker(MarkerType.Cover, path.Start, path.Piece, path.Direction));

                    if (!piece.IsEmpty) break;
                }
            }
            m_chain.Apply(markings, path);
        }
    }
}