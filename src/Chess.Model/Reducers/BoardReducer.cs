using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Stores;
using Move = Chess.Model.Models.Move;

namespace Chess.Model.Reducers
{
    public class BoardReducer : IReducer<BoardStore>
    {
        public BoardStore Apply(IAction action, BoardStore store)
        {
            var next = store;
            switch (action)
            {
                case InitializeAction ia:
                {
                    next = BoardFactory.Instance.Create(ia.Version);
                    next.History = Enumerable.Empty<MoveHistoryItem>();
                    break;
                }
                case MoveAction ma:
                {
                    if (store.Board.GetAvailable(ma.From).Contains(ma.To))
                    {
                        var board = store.Board.DeepCopy();
                        var taken = Move(board, ma.From, ma.To);
                        next = new BoardStore
                        {
                            Board = board,
                            Captured = store.Captured.ToDictionary(pair => pair.Key, pair =>
                                {
                                    var captured = pair.Value.ToList();

                                    if(pair.Key == taken.Color) captured.Add(taken);

                                    return (IEnumerable<Piece>) captured;
                                }
                            ),
                            History = store.History.Append(new MoveHistoryItem
                            {
                                Start = store.Board.DeepCopy(),
                                Move = new Move
                                {
                                    Piece = store.Board[ma.From.X, ma.From.Y],
                                    From = ma.From,
                                    To = ma.To
                                }
                            })
                        };
                    }
                    else
                    {
                        throw new InvalidOperationException($"{ma.From} to {ma.To} is not a valid move.");
                    }
                    break;
                }
            }
            return next;
        }
        
        private static Piece Move(Board board, Point @from, Point to)
        {
            //TODO: Checking about validity
            var taken = board[to.X, to.Y];

            board[to.X, to.Y] = board[@from.X,@from.Y];
            board[to.X, to.Y].Moved();
            board[@from.X,@from.Y] = Piece.CreateEmpty();
            board.Update();

            return taken;
        }
    }
}
