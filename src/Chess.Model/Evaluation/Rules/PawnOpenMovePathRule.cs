using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    public class PawnOpenMovePathRule : AbstractPathRule
    {
        /// <inheritdoc />
        public PawnOpenMovePathRule(IPathRule chain) : base(chain)
        {
        }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Piece.Type == PieceType.Pawn && !path.Piece.HasMoved)
            {
                
            }
            base.Apply(markings, path);
        }
    }    
}
