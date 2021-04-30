using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class PawnOpenMovePathRule : MovePathRule
    {
        /// <inheritdoc />
        public PawnOpenMovePathRule(IPathRule chain) : base(chain) { }

        private bool IsPawnOpen(Path path) => !path.AllowTake && path.Piece.Type == PieceType.Pawn && !path.Piece.HasMoved;

        /// <inheritdoc />
        protected override bool Applies(Path path) => IsPawnOpen(path) && base.Applies(path);

        /// <inheritdoc />
        protected override bool Continue(Path path) => !IsPawnOpen(path);

        /// <inheritdoc />
        protected override IMove GetMove(SimpleMove move, int index) => index > 0 ? (IMove)new PawnOpenMove(move) : move;
    }
}
