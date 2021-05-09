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
        public object GetMoves(int gameId, int x, int y)
        {
            var game = m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var available = game.Markings.GetMarkers<MoveMarker>(new Point(x,y)).Select(move => move.Move.To);

            return game.Board.IsOnBoard(x, y) ? (object)Ok(available) : BadRequest(Enumerable.Empty<Location>());
        }

        [HttpPost]
        public object PostMove(int gameId, [FromBody] Move m)
        {
            var game = m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var newState = Evaluator.Instance.Move(game, m.From, m.To);

            var resp =  m_translator.FromModel(gameId, newState);
            return newState != game ? (object)Ok(resp) : BadRequest(resp);
        }
    }
}