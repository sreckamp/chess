using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Rules
{
    public class PawnPromotionMoveRule : MovePathRule
    {
        public PawnPromotionMoveRule(IPathRule chain) : base(chain) { }

        /// <inheritdoc />
        protected override bool Applies(Path path) => path.Piece.Type == PieceType.Pawn && path.OppositeEdge;
        
        /// <inheritdoc />
        protected override bool Continue(Path path) => !Applies(path);

        /// <inheritdoc />
        protected override IMove GetMove(SimpleMove move, int index) => new PawnPromotionMove(move);
    }
}
