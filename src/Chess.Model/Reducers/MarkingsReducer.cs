using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Models.Board;
using Chess.Model.Rules;
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
                case MoveAction _:
                    // clear the markings here (setup some rules for clearing the markers so enpassant markers are only 
                    // cleared at the start of the players turn
                    return (board, new MarkingStore());
                case EvaluateBoardAction _:
                    return (board, EvaluateBoard(board, markings));
                default:
                    return store;
            }
        }

        private MarkingStore EvaluateBoard(IPieceEnumerationProvider board, MarkingStore store)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = store.DeepClone();

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
        
        // private static MarkingStore UpdateAvailableMoves(IPieceEnumerationProvider board, MarkingStore store, IRules movementRules)
        // {
        //     var sw = new Stopwatch();
        //     sw.Start();
        //
        //     var next = store.DeepClone();
        //     foreach (var (point, piece) in board)
        //     {
        //         movementRules.Apply(point, piece, board, next);
        //     }
        //
        //     sw.Stop();
        //     Console.WriteLine($"Updated Available Moves in {sw.ElapsedMilliseconds}mS");
        //
        //     return next;
        // }
    }
}