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
            var markers = store.Board.SelectMany(square => store.Markings
                    .GetMarkers<IDirectionalMarker>(square.Item1)
                    .Where(marker => includeMoves || marker.Type != MarkerType.Move)
                    .Select(marker => (marker is MoveMarker move ? move.Move.To : square.Item1, marker)))
                .GroupBy(kvp => kvp.Item1)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(tuple => tuple.Item2));
                 
            return new GameState
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
                                    Markers = markers.ContainsKey(square.Item1)
                                        ? markers[square.Item1].Select(marker =>  
                                            new Marker
                                            {
                                                Type = marker.Type.ToString().ToLower(),
                                                SourceColor = store.Board[marker.Source].Color.ToString().ToLower(),
                                                SourceType = store.Board[marker.Source].Type.ToString().ToLower(),
                                                Direction = marker.Direction.ToString().ToLower()
                                            })
                                        : Enumerable.Empty<Marker>()
                                }
                            },
                            Color = square.Item2.Color.ToString().ToLower(),
                            Type = square.Item2.Type.ToString().ToLower(),
                        }),

                Corners = store.Board.Corners,
                Size = store.Board.Size
            };
        }
    }
}