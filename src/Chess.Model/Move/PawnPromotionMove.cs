using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public class PawnPromotionMove : SimpleMove
    {
        public Piece Apply(GameBoard board)
        {
            //TODO: Is Opposite Rank (On farthest Rank or against the corner)
            return base.Apply(board);
        }
    }
}