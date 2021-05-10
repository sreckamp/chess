using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model;
using Chess.Model.Evaluation.Models;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private readonly IGameTranslator m_translator;

        public MovesController(IGameProviderService gameProvider, IGameTranslator translator)
        {
            m_gameService = gameProvider;
            m_translator = translator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetMoves(int gameId, int x, int y)
        {
            var game = m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            if (!game.Board.IsOnBoard(x, y))
            {
                return BadRequest($"({x}, {y}) is not on the board.");
            }

            return game.Markings
                .GetMarkers<MoveMarker>(new Point(x,y)).Select(move => (Location)move.Move.To).ToList();
        }

        [HttpPost]
        public ActionResult<GameState> PostMove(int gameId, [FromBody] Move m)
        {
            var game = m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var newState = Evaluator.Instance.Move(game, m.From, m.To);

            if (newState == game)
            {
                return BadRequest($"{m} is not a valid move.");
            }

            m_gameService.Update(gameId, newState);
            return m_translator.FromModel(gameId, newState);
        }
    }
}