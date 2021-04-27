﻿using System;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Check if the path includes a king moving into check
    /// </summary>
    public sealed class MoveIntoCheckPathRule : AbstractPathRule
    {
        public MoveIntoCheckPathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Piece.Type == PieceType.King &&
                path.Squares.Any(square => markings.GetMarkers<SimpleMarker>(square.Item1, MarkerType.Cover)
                    .Any(marker => marker.Piece.Color != path.Piece.Color)))
            {
                // Reject this move
                return;
            }

            base.Apply(markings, path);
        }
    }
}
