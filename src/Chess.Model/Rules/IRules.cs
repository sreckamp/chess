using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

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