using System.Drawing;

//TODO: en passant (pawn makes first move two squares, next turn opponent attacking one square in front of start takes where it would have been and takes)
//TODO: pawn promotion (pawn on last rank becomes a queen (Do I need the option to choose?))
//TODO: castling (king move 2 sqares toward rook & rook jumps over.  First move for both king & rook)
//TODO: cannot reveal check (pinning)
//TODO: cannot move into check
//TODO: must move out of check.
//TODO: Checkmate
//TODO: Take control
namespace Chess.Model.Models.Rules
{
    public interface IMovementRule
    {
        int MinCount { get; }

        int MaxCount { get; }

        Point GetResult(Point start, Direction dir, int count);
    }
}