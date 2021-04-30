using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Extensions;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Limit Squares to only those that prevents the king from being in check
    /// </summary>
    public class KingInCheckMoveRule : AbstractPathRule
    {
        public KingInCheckMoveRule(IPathRule chain) : base(chain) { }

        public override void Apply(IMarkingsProvider markings, Path path)
        {
            var checkMarkers = markings.GetKingMarkers<CheckMarker>(path.Piece.Color).ToList();

            base.Apply(checkMarkers.Any() ? new MarkingFilter(marker =>
            {
                if (!(marker is MoveMarker moveMarker) || path.Piece.Type == PieceType.King)
                {
                    //   + All King moves
                    return true;
                }
                if (checkMarkers.Count > 1)
                {
                    //   + If multiple checks, must move king
                    return false;
                }

                var move = moveMarker.Move;

                var check = checkMarkers[0];

                //   + Take single checking piece
                if (move.To == check.Source)
                {
                    return true;
                }

                //   + If "None" must take piece or move king otherwise intercede
                return check.Direction != Direction.None && move.To.IsBetween(check.Source, check.KingLocation);
            }, markings) : markings, path);
        }
    }
}