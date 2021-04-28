namespace Chess.Model.Evaluation.Rules
{
    public static class PathRules
    {
        /// <summary>
        /// Rule chain to mark squares.
        /// </summary>
        public static readonly IPathRule MarkRules =
            new FilterPathRule((path => path.AllowTake),
                new MarkCheckPathRule(
                    new MarkCoverPathRule(
                        new MarkPinPathRule(NopPathRule.Instance))));

        /// <summary>
        /// Rule chain to identify available moves.
        /// </summary>
        public static readonly IPathRule MoveRules =
            new KingInCheckMoveRule(
                new MoveIntoCheckPathRule(
                    new CastleMovePathRule(
                        new PawnOpenMovePathRule(
                            new PawnPromotionMoveRule(
                                new PinMovePathRule(
                                    new EnPassantTakeMovePathRule(
                                        new MovePathRule(NopPathRule.Instance))))))));
    }
}
