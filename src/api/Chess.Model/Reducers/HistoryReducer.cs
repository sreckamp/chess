// using System.Linq;
// using Chess.Model.Actions;
// using Chess.Model.Models;
// using Chess.Model.Move;
// using Chess.Model.Stores;
//
// namespace Chess.Model.Reducers
// {
//     public sealed class MovesReducer : IReducer<MovesStore>
//     {
//         public MovesStore Apply(IAction action, MovesStore store)
//         {
//             switch (action)
//             {
//                 case InitializeAction _:
//                 {
//                     return new MovesStore
//                     {
//                         History = Enumerable.Empty<MoveHistoryItem>()
//                     };
//                 }
//                 case MoveAction ma:
//                 {
//                     return new MovesStore
//                     {
//                         History = store.History.Append(new MoveHistoryItem
//                         {
//                             Start = store.Board,
//                             Move = new SimpleMove
//                             {
//                                 From = store.Board.GetSquare(ma.From),
//                                 To = store.Board.GetSquare(ma.To)
//                             },
//                             Taken = store.Board[ma.From],
//                             Checks = Enumerable.Empty<Color>()
//                         })
//                     };
//                 }
//                 default:
//                     return store;
//             }
//         }
//     }
// }