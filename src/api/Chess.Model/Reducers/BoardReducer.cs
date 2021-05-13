using System;
using System.Diagnostics;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Reducers
{
    public sealed class BoardReducer : IReducer<GameBoard>
    {
        public GameBoard Apply(IAction action, GameBoard store)
        {
            return action switch
            {
                InitializeAction ia => ia.Board ?? BoardFactory.Instance.Create(ia.Version),
                MoveAction ma => Move(store, ma.Move),
                NextPlayerAction npa => RemoveKing(store, npa.CurrentColor),
                _ => store
            };
        }

        private static GameBoard RemoveKing(GameBoard board, Color color)
        {
            return board.Filter((point, piece) => piece.Type != PieceType.King || piece.Color != color);
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
