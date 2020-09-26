using System;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    /// <summary>
    /// Special move that a pawn can make when it has not been moved.  This moves two squares and marks enPassant
    /// </summary>
    public class PawnOpenMove : SimpleMove
    {
        public override Piece Apply(GameBoard board)
        {
            var result = base.Apply(board);

            board.GetSquare(From.CartesianOffset(To).Divide(2)).Mark(
                new SimpleMarker(MarkerType.EnPassant, board.GetSquare(To), Direction.None));

            return result;
        }
        
        public override IMove Clone() => new PawnOpenMove
        {
            Piece = new Piece(Piece.Type, Piece.Color, Piece.Edge),
            From = From,
            To = To
        };
    }
}