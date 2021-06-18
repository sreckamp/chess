using System;
using System.Threading.Tasks;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("api/games/{gameId:int}/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;

        public PlayersController(IGameProviderService gameService)
        {
            m_gameService = gameService;
        }

        [HttpPost]
        public async Task<ActionResult<PlayerColorAssignment>> PostPlayer(int gameId, [FromBody] Player m)
        {
            var game = await m_gameService.GetGame(gameId);

            if (game == null)
            {
                return NotFound($"Game {gameId} does not exist.");
            }

            var color = m_gameService.ColorForConnectionId(gameId, m.ConnectionId).ToString();
            Console.WriteLine($"{m.ConnectionId} <= {color} [{m_gameService.GetType().Name}]");

            return new PlayerColorAssignment
            {
                ConnectionId = m.ConnectionId,
                Color = color
            };
        }
    }
}