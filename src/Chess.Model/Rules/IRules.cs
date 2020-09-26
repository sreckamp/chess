using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

//TODO: pawn promotion (pawn on last rank becomes a queen (Do I need the option to choose?))
//TODO: must move out of check.
//TODO: Checkmate
//TODO: Take control
namespace Chess.Model.Rules
{
    public interface IRules
    {
        bool Applies(PieceType type);
        void Apply(Square square, ISquareProvider squares);
    }
}