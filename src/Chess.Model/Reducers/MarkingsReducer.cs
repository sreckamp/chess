using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Rules;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using Chess.Model.Move;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public class MarkingsReducer : IReducer<(IPieceEnumerationProvider, MarkingStore)>
    {
        private readonly IEnumerable<IPathRule> m_rules;
        private readonly IEnumerable<IPathSource> m_pathSources;

        public MarkingsReducer(IEnumerable<IPathRule> rules, IEnumerable<IPathSource> pathSources)
        {
            m_rules = rules;
            m_pathSources = pathSources;
        }

        public (IPieceEnumerationProvider, MarkingStore) Apply(IAction action, (IPieceEnumerationProvider, MarkingStore) store)
        {
            var (board, markings) = store;

            switch (action)
            {
                case InitializeAction _:
                    return (board, new MarkingStore());
                case MoveAction ma:
                    return (board, MarkForMoves(board, ma.Move, markings));
                case EvaluateBoardAction eba:
                    return (board, EvaluateBoard(board, eba.ActivePlayer, markings));
                default:
                    return store;
            }
        }

        private MarkingStore MarkForMoves(IPieceEnumerationProvider board, IMove move, MarkingStore store)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = store.DeepClone();

            if (move is PawnOpenMove open)
            {
                var pawn = board.First(tuple => tuple.Item1 == move.From).Item2;
                next.Mark(open.EnPassant, new SimpleMarker(MarkerType.EnPassant, move.To, pawn, Direction.None));
            }

            sw.Stop();
            Console.WriteLine($"Updated Markings for Move in {sw.ElapsedMilliseconds}mS");
        
            return next;
        }

        private MarkingStore EvaluateBoard(IPieceEnumerationProvider board, Color activePlayer, MarkingStore store)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = store.Filter(marker => marker.Type == MarkerType.EnPassant && ((SimpleMarker)marker).Piece.Color != activePlayer);
            next.InCheck.Clear();

            foreach (var rule in m_rules)
            {
                foreach (var (point, piece) in board)
                {
                    foreach (var path in m_pathSources
                        .SelectMany(source => source.GetPaths(point, piece, board)))
                    {
                        rule.Apply(next, path);
                    }
                }
            }
        
            sw.Stop();
            Console.WriteLine($"Updated Markings in {sw.ElapsedMilliseconds}mS");
        
            return next;
        }
    }
}