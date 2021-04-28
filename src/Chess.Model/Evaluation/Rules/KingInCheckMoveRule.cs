using Chess.Model.Evaluation.Models;

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
            base.Apply(new MarkingFilter(tuple =>
            {
                var (marks, point) = tuple;

                // If in check:
                // - Remove all moves that don't fit
                //   + All King moves
                //   + If multiple checks, must move king
                //   + If "None" must take piece or move king
                //   + Can take piece, move king, or block attack

                // Interceding move, square has a CheckMarker and the point is in the direction opposite of check from
                // the king location.
                return true;
            }, markings), path);
        }
    }
}