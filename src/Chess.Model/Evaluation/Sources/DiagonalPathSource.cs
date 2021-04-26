using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class DiagonalPathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.Bishop || piece.Type == PieceType.Queen
                ? Directions.Diagonals
                    .Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction => new Path(GetType().Name)
                {
                    AllowMove = true,
                    AllowTake = true,
                    Direction = direction,
                    Start = start,
                    Piece = piece,
                    Squares = squares.EnumerateStraightLine(start, direction)
                }) 
                : Enumerable.Empty<Path>();
    }
}