using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

//TODO: must move out of check.
//TODO: Checkmate
//TODO: Take control
namespace Chess.Model.Rules
{
    public interface IRules
    {
        /// <summary>
        /// Operation starting at a particular square.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="piece"></param>
        /// <param name="squares"></param>
        void Apply(Point start, Piece piece, IPieceEnumerationProvider squares, MarkingStore store);
    }
}