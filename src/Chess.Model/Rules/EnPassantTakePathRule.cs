using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public class EnPassantTakePathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public EnPassantTakePathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (path.AllowTake && start.Piece.Type == PieceType.Pawn && path.Moves.Any())
            {
                var move = path.Moves.First();
                var target = squares.GetSquare(move.To);

                if (target.GetMarkers(MarkerType.EnPassant).FirstOrDefault() is SimpleMarker marker
                    && start.Piece.Color != marker.Source.Piece.Color)
                {
                    start.Available = start.Available.Append(new EnPassantTakeMove
                    {
                        From = move.From,
                        To = move.To,
                        PawnLocation = marker.Source.Location
                    });
                    return;
                }
            }
            m_chain.Apply(start, path, squares);
        }
    }
}