using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class MoveIntoCheckPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public MoveIntoCheckPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (start.Piece.Type == PieceType.King &&
                path.Moves.Any() &&
                (IsInDirectionOfCheck(start, path.Direction) ||
                 IsCoveredAgainst(squares.GetSquare(path.Moves.First().To), start.Piece.Color)))
            {
                // Reject this move
                return;
            }

            m_chain.Apply(start, path, squares);
        }

        private static bool IsInDirectionOfCheck(Square start, Direction direction) => start.GetMarkers(MarkerType.Check)
            .Any(marker => marker.Direction == direction && marker.Source.Piece.Type != PieceType.Pawn);
        
        private static bool IsCoveredAgainst(Square square, Color color) => square.GetMarkers(MarkerType.Cover)
            .Any(marker => marker.Source.Piece.Color != color);
    }
}
