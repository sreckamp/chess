namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Limit Squares to only those that prevents the king from being in check
    /// </summary>
    public class KingInCheckRule : AbstractPathRule
    {
        public KingInCheckRule(IPathRule chain) : base(chain) { }

        // If in check:
        // - Remove all moves that don't fit
        //   + All King moves
        //   + If multiple checks, must move king
        //   + If "None" must take piece or move king
        //   + Can take piece, move king, or block attack
    }
}