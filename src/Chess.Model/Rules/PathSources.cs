﻿using System.Collections.Generic;

namespace Chess.Model.Rules
{
    public static class PathSources
    {
        public static readonly IEnumerable<IPathSource> Sources = new IPathSource[]
        {
            new KingPathSource(),
            new RankPathSource(), 
            new PawnMovePathSource(),
            new PawnTakePathSource(),
            new DiagonalPathSource(),
            new CardinalPathSource(),
            new KnightPathSource(),
        };
    }
}