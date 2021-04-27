namespace Chess.Model.Evaluation.Rules
{
    public static class PathRules
    {
        /// <summary>
        /// Rule chain to mark squares.
        /// </summary>
        public static readonly IPathRule MarkRules =
            new FilterPathRule((path => path.AllowTake),
                new CheckPathRule(
                    new CoverPathRule(
                        new PinMarkPathRule(NopPathRule.Instance))));

        /// <summary>
        /// Rule chain to identify available moves.
        /// </summary>
        public static readonly IPathRule MoveRules =
            new MoveIntoCheckPathRule(
                new PawnOpenMovePathRule(
                    new PawnPromotionRule(
                        new PinPathRule(
                            new EnPassantTakePathRule(
                                new MovePathRule(
                                    new TakePathRule(NopPathRule.Instance)))))));
    }
}
