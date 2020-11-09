using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

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
        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Piece.Type == PieceType.King &&
                path.Squares.Any(square => markings.GetMarkers<SimpleMarker>(path.Start, MarkerType.Cover)
                    .Any(marker => marker.Piece.Color != path.Piece.Color)))
            {
                // Reject this move
                return;
            }

            m_chain.Apply(markings, path);
        }

        // /// <summary>
        // /// TODO: Make this part of marking the squares.  Once you hit a king of another color, keep going till you hit the end of a board
        // /// </summary>
        // /// <param name="start"></param>
        // /// <param name="direction"></param>
        // /// <returns></returns>
        // private static bool IsInDirectionOfCheck(Square start, Direction direction) => start.GetMarkers(MarkerType.Check)
        //     .Any(marker => marker.Direction == direction && marker.Source.Piece.Type != PieceType.Pawn);
        
        // private static bool IsCoveredAgainst(Square square, Color color) => square.GetMarkers(MarkerType.Cover)
        //     .Any(marker => marker.Source.Piece.Color != color);
    }
}
