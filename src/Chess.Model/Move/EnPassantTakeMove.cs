using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    public class EnPassantTakeMove : IMove
    {
        public Point From { get; set; }
        public Point To { get; set; }
        public Point PawnLocation { get; set; }
        public Piece Apply(GameBoard board)
        {
            var taken = board[PawnLocation];

            board[To] = board[From];
            board[To].Moved();
            board[From] = Piece.CreateEmpty();
            board[PawnLocation] = Piece.CreateEmpty();

            return taken;
        }

        public IMove Clone() => new EnPassantTakeMove
        {
            From = From,
            To = To,
            PawnLocation = PawnLocation
        };
    }
}