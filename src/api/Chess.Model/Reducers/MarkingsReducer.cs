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

            return action switch
            {
                InitializeAction _ => (board, new MarkingStore()),
                MoveAction ma => (board, MarkForMoves(board, ma.Move, markings)),
                EvaluateBoardAction eba => (board, EvaluateBoard(board, eba.CurrentColor, markings)),
                _ => store
            };
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
            next.KingLocations.Clear();

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

            next.AvailableColors = GetAvailableColors(board, next);

            sw.Stop();
            Console.WriteLine($"Updated Markings in {sw.ElapsedMilliseconds}mS");
        
            return next;
        }
        
        private IEnumerable<Color> GetAvailableColors(IPieceEnumerationProvider board, MarkingStore store)
            => board.Where(tuple => store.GetMarkers<MoveMarker>(tuple.Item1).Any())
                .GroupBy(tuple => tuple.Item2.Color)
                .Select(group => group.Key).Distinct().Where(color => color != Color.None);
    }
}