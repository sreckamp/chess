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
    [Route("api/games/{gameId:int}/moves")]
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
            var game = await m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            if (!game.Store.Board.IsOnBoard(x, y))
            {
                return BadRequest($"({x}, {y}) is not on the board.");
            }

            if (game.Store.Markings.KingLocations.Count == 1)
            {
                return NoContent();
            }

            return game.Store.Markings
                .GetMarkers<MoveMarker>(new Point(x,y)).Select(move => (Location)move.Move.To).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<GameState>> PostMove(int gameId, [FromBody] Move m)
        {
            var game = await m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var newStore = Evaluator.Instance.Move(game.Store, m.From, m.To);

            if (newStore == game.Store)
            {
                return BadRequest($"{m} is not a valid move.");
            }

            var result = await m_gameService.UpdateStore(game.Id, newStore);
            await m_hubContext.Clients.All.GameUpdated(new GameUpdateMessage {Id = gameId});

            return m_translator.FromModel(result);
        }
    }
}
