using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class EnPassantTakeMovePathRule : AbstractPathRule
    {
        public EnPassantTakeMovePathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.AllowTake && path.Piece.Type == PieceType.Pawn && path.Squares.Any())
            {
                var (target, _) = path.Squares.First();

                if (markings.GetMarkers<SimpleMarker>(target, MarkerType.EnPassant).FirstOrDefault() is SimpleMarker marker
                    && path.Piece.Color != marker.Piece.Color)
                {
                    markings.Mark(path.Start, new MoveMarker(new EnPassantTakeMove(path.Start, target, marker.Source)));

                    // return;
                }
            }
            base.Apply(markings, path);
        }
    }
}