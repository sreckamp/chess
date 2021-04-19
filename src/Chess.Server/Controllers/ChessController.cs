using System.Linq;
using Chess.Model;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Piece = Chess.Server.Model.Piece;

namespace Chess.Server.Controllers
{
    [Route("chess/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private static int _gameId = 10000;

        public GamesController(IGameProviderService gameProvider)
        {
            m_gameService = gameProvider;
        }

        [HttpGet("{gameId?}")]
        public GameState GetGame(int? gameId, int players=2)
        {
            if (gameId == null)
            {
                gameId = _gameId;
                _gameId++;
                var g = new Game(players == 4 ? Version.FourPlayer : Version.TwoPlayer);
                m_gameService.StoreGame((int)gameId, g);
            }
            var id = (int) gameId;

            var game = m_gameService.GetGame(id);
            if (game.Store.Board == null)
            {
                game.Init();
            }

            return BuildResponse(id, game.Store);
        }

        internal static GameState BuildResponse(int id, GameStore store)
        {
            var state = new GameState
            {
                GameId = id,
                Name = $"Game {id}",

                CurrentPlayer = store.CurrentPlayer.ToString(),

                // ActiveLocation = store.Board.Selection,
                //
                // Available = store.Board.Available.Select(p => (Location)p),

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