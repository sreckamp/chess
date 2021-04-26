using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

namespace Chess.Model.Rules
{
    /// <summary>
    /// Evaluates path for check
    /// </summary>
    public sealed class CheckPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public CheckPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var squares = path.Squares.ToList();
                CheckMarker marker = default;

                var i = 0;

                for (; i < squares.Count; i++)
                {
                    var (target, targetPiece) = squares[i];

                    if (targetPiece.IsEmpty) continue;

                    if (targetPiece.Color == path.Piece.Color ||
                        targetPiece.Type != PieceType.King) break;

                    marker = new CheckMarker(path.Start, target, path.Direction);
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

            m_chain.Apply(markings, path);
        }
    }
}