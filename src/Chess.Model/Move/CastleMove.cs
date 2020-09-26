using System;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public class CastleMove : SimpleMove
    {
        public SimpleMove RookMove { get; set; }

        public override Piece Apply(GameBoard board)
        {
            base.Apply(board);
            return RookMove.Apply(board);
        }

        public override IMove Clone() => new CastleMove
        {
            Piece = new Piece(Piece.Type, Piece.Color, Piece.Edge),
            From = From,
            To = To,
            RookMove = RookMove.SimpleMoveClone()
        };

        public override string ToString() =>
            Math.Abs(RookMove.From.X - RookMove.To.X) + Math.Abs(RookMove.From.Y - RookMove.To.Y) > 2 ? "O-O-O" : "O-O";
    }
}
