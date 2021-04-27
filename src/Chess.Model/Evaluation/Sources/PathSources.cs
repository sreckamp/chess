using System.Collections.Generic;

namespace Chess.Model.Evaluation.Sources
{
    public static class PathSources
    {
        public static readonly IEnumerable<IPathSource> Sources = new IPathSource[]
        {
            new KingPathSource(),
            new RankPathSource(), 
            new PawnMovePathSource(),
            new DiagonalPathSource(),
            new CardinalPathSource(),
            new KnightPathSource(),
        };
    }
}