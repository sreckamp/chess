using System;
using System.Diagnostics;
using Chess.Model.Actions;
using Chess.Model.Models.Board;
using Chess.Model.Rules;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public class MarkingsReducer : IReducer<(IPieceEnumerationProvider, MarkingStore)>
    {
        private readonly IRules m_markingRules;
        private readonly IRules m_movementRules;

        public MarkingsReducer(IRules markingRules, IRules movementRules)
        {
            m_markingRules = markingRules;
            m_movementRules = movementRules;
        }

        public (IPieceEnumerationProvider, MarkingStore) Apply(IAction action, (IPieceEnumerationProvider, MarkingStore) store)
        {
            switch (action)
            {
                case InitializeAction _:
                    return (store.Item1, new MarkingStore());
                case UpdateMarkersAction _:
                    return (store.Item1, UpdateMarkers(store.Item1, store.Item2, m_markingRules));
                case UpdateAvailableMovesAction _:
                    return (store.Item1, UpdateAvailableMoves(store.Item1, store.Item2, m_movementRules));
                default:
                    return store;
            }
        }

        private static MarkingStore UpdateMarkers(IPieceEnumerationProvider board, MarkingStore store, IRules markingRules)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = store.DeepClone();

            foreach (var (point, piece) in board)
            {
               markingRules.Apply(point, piece, board, store);
            }
        
            sw.Stop();
            Console.WriteLine($"Updated Markings in {sw.ElapsedMilliseconds}mS");
        
            return next;
        }
        
        private static MarkingStore UpdateAvailableMoves(IPieceEnumerationProvider board, MarkingStore store, IRules movementRules)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = store.DeepClone();
            foreach (var (point, piece) in board)
            {
                movementRules.Apply(point, piece, board, store);
            }

            sw.Stop();
            Console.WriteLine($"Updated Available Moves in {sw.ElapsedMilliseconds}mS");
        
            return next;
        }
    }
}