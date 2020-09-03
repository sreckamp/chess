using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Chess.Model;
using Chess.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Piece = Chess.Server.Model.Piece;

namespace Chess.Server.Controllers
{
    [Route("chess/games")]
    public class ChessGameController : ControllerBase
    {
        private static int m_game = 10000;
        private static readonly Dictionary<int, Game> m_gameState = new Dictionary<int, Game>();

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{gameId?}")]
        public BoardState GetGame(int? gameId)
        {
            if (gameId == null)
            {
                gameId = m_game;
                m_game++;
            }

            var id = (int) gameId;

            if (!m_gameState.ContainsKey(id))
            {
                var g = new Game(ChessVersion.FourPlayer);
                g.Init();
                m_gameState[id] = g;
            }

            return BuildResponse(id, m_gameState[id]);
        }

        [HttpGet("{gameId}/moves/{color}")]
        public object GetMoves(int gameId, string color, int x, int y)
        {
            if (!m_gameState.ContainsKey(gameId))
            {
                return NotFound($"Game {gameId} has not been initialized.");
            }

            var game = m_gameState[gameId];
            var source = new Point(x, y);
            return Ok(game.GetPossibleMoves(color, source).Select(p => new Move
            {
                From = source,
                To = p
            }));
        }

        [HttpPost("{gameId}/moves/{color}")]
        public object PutMove(int gameId, string color, [FromBody] Move m)
        {
            if (!m_gameState.ContainsKey(gameId))
            {
                return NotFound($"Game {gameId} has not been initialized.");
            }

            var game = m_gameState[gameId];

            return game.Move(color, m.From, m.To) ?
                (object)Ok() : BadRequest($"{m} is not a valid move.");
        }

        private BoardState BuildResponse(int id, Game game)
        {
            return new BoardState
            {
                GameId = id,
                Name = $"Game {id}",
                Corners = game.Board.CornerSize,
                Width = game.Board.Width,
                Corner = game.Board.CornerSize % 2 == 1 ? "dark":"light",
                Other = game.Board.CornerSize % 2 == 1 ? "light":"dark",
                RotationMap = new Dictionary<string, string>()
                {
                    //TODO: generalize this
                    {"white", "none"},
                    {"silver", "counterclockwise"},
                    {"black", "upsidedown"},
                    {"gold", "clockwise"}
                },
                Pieces = game.Board.Placements.Select(placement => new Piece
                    {
                        Location = placement.Location,
                        Type = placement.Piece.Name,
                        Color = placement.Piece.Team
                    }) .ToArray()
            };
        }
    }
}