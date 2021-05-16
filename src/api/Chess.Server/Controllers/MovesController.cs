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
        public async Task<ActionResult<IEnumerable<Location>>> GetMoves(int gameId, int x, int y)
        {
            var store = await m_gameService.GetGame(gameId);

            if (store == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            if (!store.Board.IsOnBoard(x, y))
            {
                return BadRequest($"({x}, {y}) is not on the board.");
            }

            if (store.Markings.KingLocations.Count == 1)
            {
                return NoContent();
            }

            return store.Markings
                .GetMarkers<MoveMarker>(new Point(x,y)).Select(move => (Location)move.Move.To).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<GameState>> PostMove(int gameId, [FromBody] Move m)
        {
            var store = await m_gameService.GetGame(gameId);

            if (store == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var newState = Evaluator.Instance.Move(store, m.From, m.To);

            if (newState == store)
            {
                return BadRequest($"{m} is not a valid move.");
            }

            await m_gameService.Update(gameId, newState);
            await m_hubContext.Clients.All.GameUpdated(new GameUpdateMessage {Id = gameId});

            return m_translator.FromModel(gameId, newState);
        }
    }
}
