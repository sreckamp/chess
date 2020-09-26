using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public interface IPathRule
    {
        /// <summary>
        /// Execute the rule against the given path.
        /// </summary>
        /// <param name="start">The square where the path starts</param>
        /// <param name="path">The sequence of moves that are available</param>
        /// <param name="squares">The provider that supplies squares for processing</param>
        void Apply(Square start, Path path, ISquareProvider squares);
    }
}