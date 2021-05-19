using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;
using Version = Chess.Model.Models.Version;
using ModelPiece = Chess.Model.Models.Piece;

namespace Chess.Server.Model
{
    public class GameTranslator: IGameTranslator
    {
        /// <inheritdoc/>
        public Game ToModel(GameState state)
        {
            var builder = new BoardBuilder()
                .SetSize(state.Size)
                .SetCorners(state.Corners);
            
            foreach (var piece in state.Pieces)
            {
                builder.AddPiece(piece.Location.X, piece.Location.Y, piece.Color.ToColor(), piece.Type.ToPieceType());
            }

            return new Game
            {
                Id = state.GameId,
                Name = state.Name,
                Store = new GameStore
                {
                    Board = builder.Build(),
                    Markings = new MarkingStore(),
                    Version = state.Size > 8 ? Version.FourPlayer : Version.TwoPlayer,
                    CurrentColor = state.CurrentPlayer.ToColor()
                }
            };
        }

        /// <inheritdoc/>
        public GameState FromModel(Game game, bool includeMoves)
        {
            var markers = game.Store.Board.SelectMany(square => game.Store.Markings
                    .GetMarkers<IDirectionalMarker>(square.Item1)
                    .Where(marker => includeMoves || marker.Type != MarkerType.Move)
                    .Select(marker => (marker is MoveMarker move ? move.Move.To : square.Item1, marker)))
                .GroupBy(kvp => kvp.Item1)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(tuple => tuple.Item2));
                 
            return new GameState
            {
                GameId = game.Id,
                Name = game.Name,

                CurrentPlayer = game.Store.CurrentColor.ToString(),

                Pieces = game.Store.Board.Where(square => !square.Item2.IsEmpty
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
                                                SourceColor = game.Store.Board[marker.Source].Color.ToString().ToLower(),
                                                SourceType = game.Store.Board[marker.Source].Type.ToString().ToLower(),
                                                Direction = marker.Direction.ToString().ToLower()
                                            })
                                        : Enumerable.Empty<Marker>()
                                }
                            },
                            Color = square.Item2.Color.ToString().ToLower(),
                            Type = square.Item2.Type.ToString().ToLower(),
                        }),

                Corners = game.Store.Board.Corners,
                Size = game.Store.Board.Size
            };
        }
    }
}