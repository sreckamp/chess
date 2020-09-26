using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class MoveIntoCheckPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public MoveIntoCheckPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (start.Piece.Type == PieceType.King)
            {
                var move = path.Moves.FirstOrDefault();
                if (move != null && squares.GetSquare(move.To).GetMarkers(MarkerType.Cover)
                    .Cast<SimpleMarker>().Any(marker => marker.Source.Piece.Color != start.Piece.Color))
                {
                    return;
                }
            }
            m_chain.Apply(start, path, squares);
        }
    }
}