using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Rules;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class EnPassantTakePathRule : AbstractPathRule
    {
        public EnPassantTakePathRule(IPathRule chain): base(chain) { }

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