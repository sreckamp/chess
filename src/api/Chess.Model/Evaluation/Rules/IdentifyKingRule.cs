using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    public class IdentifyKingRule : AbstractPathRule
    {
        public IdentifyKingRule(IPathRule chain) : base(chain) { }

        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Piece.Type == PieceType.King)
            {
                markings.KingLocations[path.Piece.Color] = path.Start;
            }
            base.Apply(markings, path);
        }
    }
}