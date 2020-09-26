using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Rules;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Reducers
{
    public class BoardReducer : IReducer<GameBoard>
    {
        private readonly IRules m_markingRules;
        private readonly IRules m_movementRules;

        public BoardReducer(IRules markingRules, IRules movementRules)
        {
            m_markingRules = markingRules;
            m_movementRules = movementRules;
        }

        public GameBoard Apply(IAction action, GameBoard store)
        {
            switch (action)
            {
                case InitializeAction ia:
                    return ia.Board;
                case MoveAction ma:
                    return Move(store, ma.From, ma.To);
                case UpdateMarkersAction uma:
                    return UpdateMarkings(store, m_markingRules, uma.ActivePlayer);
                case UpdateAvailableMovesAction _:
                    return UpdateAvailableMoves(store, m_movementRules);
                default:
                    return store;
            }
        }

        public static GameBoard Move(GameBoard board, Point from, Point to)
        {
            if (board.GetAvailable(from).Any(move => move.To ==to))
            {
                var sw = new Stopwatch();
                var next = board.DeepCopy();
                sw.Start();
 
                var move = next.GetAvailable(from).First(mv => mv.To == to);

                move.Apply(next);

                sw.Stop();
                Console.WriteLine($"Moved in {sw.ElapsedMilliseconds}mS");

                return next;
            }
            else
            {
                throw new InvalidOperationException($"{from} to {to} is not a valid move.");
            }
        }
        
        public static GameBoard UpdateMarkings(GameBoard board, IRules markingRules, Color activePlayer)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            // Clone the board & only keep the EnPassant that are not the current player
            var next = board.DeepCopy(marker => marker.Type == MarkerType.EnPassant &&
                                                marker.Source.Piece.Type == PieceType.Pawn &&
                                                marker.Source.Piece.Color != activePlayer);
            foreach (var square in next)
            {
                markingRules.Apply(square, next);
            }

            sw.Stop();
            Console.WriteLine($"Updated Markings in {sw.ElapsedMilliseconds}mS");

            return next;
        }

        public static GameBoard UpdateAvailableMoves(GameBoard board, IRules movementRules)
        {
            var sw = new Stopwatch();
            var next = board.DeepCopy();
            sw.Start();
            foreach (var square in next)
            {
                movementRules.Apply(square, next);
            }
            sw.Stop();
            Console.WriteLine($"Updated Available Moves in {sw.ElapsedMilliseconds}mS");

            return next;
        }
    }
}
