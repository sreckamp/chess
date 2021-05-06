using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("chess/games/{gameId}/moves")]
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
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);

            var available = game.Store.Markings.GetMarkers<MoveMarker>(new Point(x,y)).Select(move => move.Move.To);

            return game.Store.Board.IsOnBoard(x, y) ? (object)Ok(available) : BadRequest(Enumerable.Empty<Location>());
        }

        [HttpPost]
        public object PostMove(int gameId, [FromBody] Move m)
        {
            // if (!m_gameState.ContainsKey(gameId))
            // {
            //     return NotFound($"Game {gameId} has not been initialized.");
            // }

            var game = m_gameService.GetGame(gameId);
            var before = game.Store;

            game.Move(m.From, m.To);

            var resp =  m_translator.FromModel(gameId, game.Store);
            return game.Store != before ? (object)Ok(resp) : BadRequest(resp);

            // return game.Move(m.To) ?
            //     (object)Ok() : BadRequest($"{m} is not a valid move.");
        }
    }
}