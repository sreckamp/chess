using System;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Stores;
using Version = Chess.Model.Models.Version;
using ModelPiece = Chess.Model.Models.Piece;

namespace Chess.Server.Model
{
    public class GameTranslator: IGameTranslator
    {
        private static Color ToColor(string name)
        {
            name = char.ToUpper(name[0]) + name[1..];
            return Enum.Parse<Color>(name);
        }

        private static PieceType ToPieceType(string name)
        {
            name = char.ToUpper(name[0]) + name[1..];
            return Enum.Parse<PieceType>(name);
        }

        /// <inheritdoc/>
        public (int, GameStore) ToModel(GameState state)
        {
            var store = new GameStore
            {
                Board = BoardStoreFactory.Instance.CreateEmpty(state.Size > 8 ? Version.FourPlayer : Version.TwoPlayer),
                Markings = new MarkingStore(),
                Version = state.Size > 8 ? Version.FourPlayer : Version.TwoPlayer,
                CurrentPlayer = ToColor(state.CurrentPlayer)
            };

            foreach (var piece in state.Pieces)
            {
                var color = ToColor(piece.Color);
                var edge = BoardStoreFactory.Instance.DirectionFromColor(color);
                store.Board[piece.Location.X, piece.Location.Y] = new ModelPiece(ToPieceType(piece.Type), color, edge);
            }
            return (state.GameId, store);
        }

        /// <inheritdoc/>
        public GameState FromModel(int id, GameStore store, bool includeMoves)
        {
            //TODO: Preserve the source or the mar
            var markers = store.Board.SelectMany(square => store.Markings
                    .GetMarkers<IDirectionalMarker>(square.Item1)
                    .Where(marker => includeMoves || marker.Type != MarkerType.Move)
                    .Select(marker => (marker is MoveMarker move ? move.Move.To : square.Item1, marker)))
                .GroupBy(kvp => kvp.Item1)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(tuple => tuple.Item2));
                 
            var state = new GameState
            {
                GameId = id,
                Name = $"Game {id}",

                CurrentPlayer = store.CurrentPlayer.ToString(),

                Pieces = store.Board.Where(square => !square.Item2.IsEmpty
                                                     || markers.ContainsKey(square.Item1) && markers[square.Item1].Any()).Select(
                    square => new Piece
                        {
                            Location = new Location
                            {
                                X = square.Item1.X,
                                Y = square.Item1.Y,
                                Metadata = new Metadata
                                {
                                    Markers = markers[square.Item1].GroupBy(marker => marker.Direction)
                                        .ToDictionary(grouping => grouping.Key,
                                            grouping =>  grouping.GroupBy(marker => store.Board[marker.Source].ToString())
                                                .ToDictionary(sourceGrouping => sourceGrouping.Key, sourceGrouping => grouping.OrderByDescending(
                                                    marker => marker.Type switch
                                                        {
                                                            MarkerType.Check => 4,
                                                            MarkerType.Pin => 3,
                                                            MarkerType.Move => 2,
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
            if (includeMoves)
            {
            }
            return state;
        }
    }
}