using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Move;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public sealed class HistoryReducer : IReducer<HistoryStore>
    {
        public HistoryStore Apply(IAction action, HistoryStore store)
        {
            switch (action)
            {
                case InitializeAction _:
                {
                    return new HistoryStore
                    {
                        History = Enumerable.Empty<MoveHistoryItem>()
                    };
                }
                case MoveAction ma:
                {
                    return new HistoryStore
                    {
                        History = store.History.Append(new MoveHistoryItem
                        {
                            Start = store.Board,
                            Move = new SimpleMove
                            {
                                Piece = store.Board[ma.From],
                                From = ma.From,
                                To = ma.To,
                            },
                            Taken = store.Board[ma.From],
                            Checks = Enumerable.Empty<Color>()
                        })
                    };
                }
                default:
                    return store;
            }
        }
    }
}