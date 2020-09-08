using System.Linq;
using Chess.Model;
using Chess.Model.Stores;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Color = Chess.Model.Models.Color;
using Move = Chess.Server.Model.Move;
using Piece = Chess.Server.Model.Piece;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Controllers
{
    [Route("chess/games")]
    public class ChessGameController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private static int _gameId = 10000;

        public ChessGameController(IGameProviderService gameProvider)
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
                g.Init();
                m_gameService.StoreGame((int)gameId, g);
            }

            var id = (int) gameId;

            return BuildResponse(id, m_gameService.GetGame(id).Store);
        }

        [HttpGet("{gameId}/moves/{color}")]
        public object GetMoves(int gameId, Color color, int x, int y)
        {
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);

            var available = game.Store.Board.Board.GetAvailable(x,y).Select(p => (Location) p);

            return game.Store.Board.Board.IsOnBoard(x, y) ? (object)Ok(available) : BadRequest(Enumerable.Empty<Location>());
        }

        [HttpPost("{gameId}/moves/{color}")]
        public object PutMove(int gameId, Color color, [FromBody] Move m)
        {
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);
            var before = game.Store;

            game.Move(m.From, m.To);

            var resp = BuildResponse(gameId, game.Store); 
            return game.Store != before ? (object)Ok(resp) : BadRequest(resp);

            // return game.Move(m.To) ?
            //     (object)Ok() : BadRequest($"{m} is not a valid move.");
        }

        private static GameState BuildResponse(int id, GameStore store)
        {
            var state = new GameState
            {
                GameId = id,
                Name = $"Game {id}",

                CurrentPlayer = store.CurrentPlayer.ToString(),

                // ActiveLocation = store.Board.Selection,
                //
                // Available = store.Board.Available.Select(p => (Location)p),

                Pieces = store.Board.Board.Where(square => !square.IsEmpty || (square.AttackedBy == null || square.AttackedBy.Count > 0)).Select(
                    square => new Piece
                        {
                            Location = new Location
                            {
                                X =square.X,
                                Y=square.Y,
                                Metadata = new Metadata
                                {
                                    AttackedBy = square.AttackedBy.Select(pair => 
                                            new AttackData
                                            {
                                                Direction = pair.Key.ToString().ToLower(),
                                                Colors = pair.Value.Select(color => color.ToString().ToLower())
                                            }
                                        ),
                                    PinDir = square.PinDir.ToString().ToLower()
                                }
                            },
                            Color = square.Piece.Color.ToString(),
                            Type = square.Piece.Type.ToString(),
                        }),

                MoveHistory = store.Board.History.Select(history => new Move
                {
                    From = history.Move.From,
                    To = history.Move.To
                }),

                Corners = store.Board.Board.Corners,
                Size = store.Board.Board.Size
            };
            return state;
        }
    }
}