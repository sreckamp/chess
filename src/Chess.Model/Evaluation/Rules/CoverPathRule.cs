﻿using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class CoverPathRule : AbstractPathRule
    {
        public CoverPathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                foreach (var (target, piece) in path.Squares)
                {
                    markings.Mark(target, new SimpleMarker(MarkerType.Cover, path.Start, path.Piece, path.Direction));

                    if (!piece.IsEmpty) break;
                }
            }
            base.Apply(markings, path);
        }
    }
}