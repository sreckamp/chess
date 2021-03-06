﻿using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Mark paths that include check
    /// </summary>
    public sealed class MarkCheckPathRule : AbstractPathRule
    {
        public MarkCheckPathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var squares = path.Squares.ToList();
                CheckMarker marker = default;
                var kingColor = Color.None;

                var i = 0;

                for (; i < squares.Count; i++)
                {
                    var (target, targetPiece) = squares[i];

                    if (targetPiece.IsEmpty) continue;

                    if (kingColor == Color.None && targetPiece.Color != path.Piece.Color &&
                        targetPiece.Type == PieceType.King)
                    {
                        kingColor = targetPiece.Color;

                        marker = new CheckMarker(path.Start, path.Piece, target, path.Direction);
                        continue;
                    }
                    // Stop on Piece of Kings Color
                    if (targetPiece.Type != PieceType.King && targetPiece.Color == kingColor
                        || targetPiece.Type == PieceType.King && targetPiece.Color != kingColor) break;
                    // Include Piece Not of Kings color
                    if (targetPiece.Type == PieceType.Empty || targetPiece.Color == kingColor) continue;
                    i++;
                    break;
                }

                if (marker != null)
                {
                    for (i--; i > -1; i--)
                    {
                        var (target, _) = squares[i];

                        markings.Mark(target, marker);
                    }

                    markings.Mark(path.Start, marker);
                }
            }

            base.Apply(markings, path);
        }
    }
}