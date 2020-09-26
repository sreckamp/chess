using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class PathSources
    {
        public static readonly IEnumerable<IPathSource> Sources = new IPathSource[]
        {
            new KingPathSource(),
            new CastlePathSource(), 
            new PawnMovePathSource(),
            new PawnTakePathSource(),
            new DiagonalPathSource(),
            new CardinalPathSource(),
            new KnightPathSource(),
        };
    }
}