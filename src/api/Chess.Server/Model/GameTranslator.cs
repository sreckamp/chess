using System;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Model;
using Piece = Chess.Server.Model.Piece;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Services
{
    public class GameTranslator: IGameTranslator
    {
        public (int, GameStore) toModel(GameState state)
        {
            var store = new GameStore
            {
                Board = BoardStoreFactory.Instance.Create(state.Size > 8 ? Version.FourPlayer : Version.TwoPlayer),
                Markings = new MarkingStore(),
                Version = state.Size > 8 ? Version.FourPlayer : Version.TwoPlayer,
                CurrentPlayer = Enum.Parse<Color>(state.CurrentPlayer)
            };
            
            return (state.GameId, store);
        }

        public GameState fromModel(int id, GameStore store)
        {
            var state = new GameState
            {
                GameId = id,
                Name = $"Game {id}",

                CurrentPlayer = store.CurrentPlayer.ToString(),

                Pieces = store.Board.Where((square) => !square.Item2.IsEmpty || store.Markings.GetMarkers<IMarker>(square.Item1).Any()).Select(
                    square => new Piece
                        {
                            Location = new Location
                            {
                                X = square.Item1.X,
                                Y = square.Item1.Y,
                                Metadata = new Metadata
                                {
                                    Markers = store.Markings.GetMarkers<IDirectionalMarker>(square.Item1)
                                        .GroupBy(marker => marker.Direction)
                                        .ToDictionary(markers => markers.Key,
                                            markers =>  markers.GroupBy(marker => store.Board[marker.Source].ToString())
                                                .ToDictionary(grouping => grouping.Key, grouping => grouping.OrderByDescending(
                                                    marker => marker.Type switch
                                                        {
                                                            MarkerType.Check => 3,
                                                            MarkerType.Pin => 2,
                                                            MarkerType.Cover => 1,
                                                            _ => 0
                                                    }).First())).SelectMany(pair => pair.Value).Select(pair =>  
                                            new Marker
                                            {
                                                Type = pair.Value.Type.ToString().ToLower(),
                                                SourceColor = store.Board[pair.Value.Source].Color.ToString().ToLower(),
                                                SourceType = store.Board[pair.Value.Source].Type.ToString().ToLower(),
                                                Direction = pair.Value.Direction.ToString().ToLower()
                                            })
                                }
                            },
                            Color = square.Item2.Color.ToString().ToLower(),
                            Type = square.Item2.Type.ToString().ToLower(),
                        }),

                // MoveHistory = store.Board.History.Select(history =>
                // {
                //     if (history.Move is SimpleMove sm)
                //     {
                //         return new Move
                //         {
                //             From = sm.From,
                //             To = sm.To
                //         };
                //     }
                //     else
                //     {
                //         var castle = (CastleMove) history.Move;
                //         return new Move
                //         {
                //             From = castle.KingMove.From,
                //             To = castle.KingMove.To
                //         };
                //     }
                // }),

                Corners = store.Board.Corners,
                Size = store.Board.Size
            };
            return state;
        }
    }
}