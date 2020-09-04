using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Chess.Model;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Color = Chess.Model.Color;
using Piece = Chess.Server.Model.Piece;

namespace Chess.Server.Controllers
{
    [Route("chess/games")]
    public class ChessGameController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private static int m_gameId = 10000;

        public ChessGameController(IGameProviderService gameProvider)
        {
            m_gameService = gameProvider;
        }

        [HttpGet("{gameId?}")]
        public GameState GetGame(int? gameId, int players=2)
        {
            if (gameId == null)
            {
                gameId = m_gameId;
                m_gameId++;
                var g = new Game(players == 4 ? ChessVersion.FourPlayer : ChessVersion.TwoPlayer);
                g.Init();
                m_gameService.StoreGame((int)gameId, g);
            }

            var id = (int) gameId;

            return BuildResponse(id, m_gameService.GetGame(id));
        }

        [HttpGet("{gameId}/moves/{color}")]
        public object GetMoves(int gameId, Color color, int x, int y)
        {
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);
            var source = new Point(x, y);
            return Ok(game.GetPossibleMoves(color, source).Select(p => new Move
            {
                From = source,
                To = p
            }));
        }

        [HttpPost("{gameId}/moves/{color}")]
        public object PutMove(int gameId, Color color, [FromBody] Move m)
        {
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);

            return game.Move(color, m.From, m.To) ?
                (object)Ok() : BadRequest($"{m} is not a valid move.");
        }

        private GameState BuildResponse(int id, Game game)
        {
            return new GameState
            {
                GameId = id,
                Name = $"Game {id}",
                Corners = game.Board.CornerSize,
                Width = game.Board.Width,
                Corner = game.Board.CornerSize % 2 == 1 ? "dark":"light",
                Other = game.Board.CornerSize % 2 == 1 ? "light":"dark",
                // RotationMap = new Dictionary<string, string>()
                // {
                //     {"white", "none"},
                //     {"silver", "counterclockwise"},
                //     {"black", "upsidedown"},
                //     {"gold", "clockwise"}
                // },
                Pieces = game.Board.Placements.Select(placement => new Piece
                    {
                        Location = placement.Location,
                        Type = placement.Piece.Type.ToString(),
                        Color = placement.Piece.Color.ToString()
                    }) .ToArray()
            };
        }
    }
}