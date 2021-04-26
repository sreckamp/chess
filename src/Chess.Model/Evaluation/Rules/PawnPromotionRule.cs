namespace Chess.Model.Evaluation.Rules
{
    // public class PawnPromotionRule : IMoveRule
    // {
    //     private readonly IMoveRule m_chain;
    //     public PawnPromotionRule(IMoveRule chain)
    //     {
    //         m_chain = chain;
    //     }
    //
    //     public IEnumerable<IMove> Apply(Path path, IEnumerable<IMove> moves)
    //         => moves;
    //
    //     //path.Piece.Type == PieceType.Pawn 
    //     //     ? m_chain.Apply(path, moves)
    //     //     : m_chain.Apply(path, moves).Select(
    //     //         move => move.To.
    //     //     );
    //     //TODO: If there are any moves for pawns that reach the edge, make them promotions.
    // }
}