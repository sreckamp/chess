using System.Collections.Generic;

namespace Chess.Model.Rules
{
    public class PathRules
    {
        /// <summary>
        /// Rule chain to mark squares.
        /// </summary>
        public static readonly IPathRule MarkRules = new CheckPathRule(new CoverPathRule(new PinMarkPathRule(NopPathRule.Instance)));

        /// <summary>
        /// Rule chain to identify available moves.
        /// </summary>
        public static readonly IPathRule MoveRules = new MoveIntoCheckPathRule(
            new EnPassantTakePathRule(
                new PinPathRule(
                    new MovePathRule(
                        new TakePathRule(NopPathRule.Instance))
                )
            )
        );
    }
}