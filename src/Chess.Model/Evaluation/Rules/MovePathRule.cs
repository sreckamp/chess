using System;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Rules;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class MovePathRule : AbstractPathRule
    {
        public MovePathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any() && path.AllowMove)
            {
                var available = path.Squares.TakeWhile(square => square.Item2.IsEmpty).ToList();
                if(available.Count > 0)
                {
                    Console.WriteLine($"Move: {path}[{available.Count()}]");
                }
                markings.Mark(path.Start, available
                    .Select(target =>
                    {
                        var (point, _) = target;
                        IMove move = new SimpleMove(path.Start, point);
                        if (path.Piece.Type == PieceType.Pawn && Math.Abs(point.X + point.Y - (path.Start.X + path.Start.Y)) > 1)
                        {
                            move = new PawnOpenMove((SimpleMove) move);
                        }

                        return new MoveMarker(move);
                    }));
            }

            base.Apply(markings, path);
        }
    }
}