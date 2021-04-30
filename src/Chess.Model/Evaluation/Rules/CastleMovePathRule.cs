using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Rules
{
    public class CastleMovePathRule : AbstractPathRule
    {
        public CastleMovePathRule(IPathRule chain) : base(chain) { }

        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Piece.Type == PieceType.King && !path.AllowTake)
            {
                var (point, piece) = path.Squares.SkipWhile((tuple, i) => tuple.Item2.IsEmpty).FirstOrDefault();
                if (!piece.HasMoved && piece.Type == PieceType.Rook && path.Piece.Color == piece.Color
                    && !markings.GetKingMarkers<CheckMarker>(path.Piece.Color).Any())
                {
                    markings.Mark(path.Start, new MoveMarker(new CastleMove(
                        new SimpleMove(path.Start, path.Start.PolarOffset(path.Direction, 2)),
                        new SimpleMove(point, path.Start.PolarOffset(path.Direction, 1))
                        )));
                }

                return;
            }
            base.Apply(markings, path);
        }
    }
}