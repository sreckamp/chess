using System;
using System.Diagnostics;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Reducers
{
    public sealed class BoardReducer : IReducer<GameBoard>
    {
        // private readonly IRules m_markingRules;
        // private readonly IRules m_movementRules;

        public BoardReducer(/*IRules markingRules, IRules movementRules*/)
        {
            // m_markingRules = markingRules;
            // m_movementRules = movementRules;
        }

        public GameBoard Apply(IAction action, GameBoard store)
        {
            switch (action)
            {
                case InitializeAction ia:
                    return ia.Board;
                case MoveAction ma:
                    return Move(store, ma.Move);
                case EvaluateBoardAction uma:
                    // return UpdateMarkings(store, m_markingRules, uma.ActivePlayer);
                case UpdateAvailableMovesAction uama:
                    // return UpdateAvailableMoves(store, m_movementRules, uama.ActivePlayer);
                default:
                    return store;
            }
        }

        private static GameBoard Move(GameBoard board, IMove move)
        {
            var sw = new Stopwatch();
            sw.Start();

            var next = board.DeepCopy();

            move.Apply(next);

            sw.Stop();
            Console.WriteLine($"Moved in {sw.ElapsedMilliseconds}mS");

            return next;
        }
    }
}
