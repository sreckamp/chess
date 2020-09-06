using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Color = Chess.Model.Models.Color;
using Move = Chess.Server.Model.Move;
using Piece = Chess.Server.Model.Piece;

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
            var before = game.Store;

            game.Select(new Point(x, y));

            var resp = BuildResponse(gameId, game.Store); 
            return game.Store != before ? (object)Ok(resp) : BadRequest(resp);
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

            game.Move(m.To);

            var resp = BuildResponse(gameId, game.Store); 
            return game.Store != before ? (object)Ok(resp) : BadRequest(resp);

            // return game.Move(m.To) ?
            //     (object)Ok() : BadRequest($"{m} is not a valid move.");
        }

        private static GameState BuildResponse(int id, GameStore store)
        {
            var pieces = new List<Piece>();
            for (var y = 0; y < store.Board.Board.Length; y++)
            {
                for (var x = 0; x < store.Board.Board[y].Length; x++)
                {
                    var piece = store.Board.Board[y][x];
                    if(piece.IsEmpty) continue;
                    pieces.Add(new Piece
                    {
                        Location = new Location { X = x, Y = y},
                        Color = piece.Color.ToString(),
                        Type = piece.Type.ToString(),
                    });
                }
            }
            return new GameState
            {
                GameId = id,
                Name = $"Game {id}",

                CurrentPlayer = store.CurrentPlayer.ToString(),

                ActiveLocation = store.Board.Selection,

                Available = store.Board.Available.Select(p => (Location)p),

                Pieces = pieces,

                MoveHistory = store.Board.History.Select(history => new Move
                {
                    From = history.Move.From,
                    To = history.Move.To
                }),

                Corners = store.Board.CornerSize,
                Size = store.Board.Size
            };
        }
    }
}