using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Chess.Model;
using Chess.Model.Evaluation.Models;
using Chess.Server.Model;
using Chess.Server.Services;
using Chess.Server.Services.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chess.Server.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private readonly IGameTranslator m_translator;
        private readonly IHubContext<GameEventHub, IGameUpdateClient> m_hubContext; 

        public MovesController(IGameProviderService gameProvider, IGameTranslator translator,
            IHubContext<GameEventHub, IGameUpdateClient> hubContext)
        {
            m_gameService = gameProvider;
            m_translator = translator;
            m_hubContext = hubContext;
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

            if (game.Markings.KingLocations.Count == 1)
            {
                return NoContent();
            }

            return game.Markings
                .GetMarkers<MoveMarker>(new Point(x,y)).Select(move => (Location)move.Move.To).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<GameState>> PostMove(int gameId, [FromBody] Move m)
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
            await m_hubContext.Clients.All.GameUpdated(new GameUpdateMessage {Id = gameId});

            return m_translator.FromModel(gameId, newState);
        }
    }
}
