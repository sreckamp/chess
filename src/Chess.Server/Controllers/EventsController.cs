using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("chess/games/{gameId}/events")]
    public class EventsController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;

        public EventsController(IGameProviderService gameProvider)
        {
            m_gameService = gameProvider;
        }

        // /// <summary>
        // /// This is used for "push" events.  When someone moves, this is triggered so the board can be updated.
        // /// </summary>
        // /// <param name="gameId"></param>
        // /// <param name="color"></param>
        // /// <returns></returns>
        // [HttpGet("{color}")]
        // public async Task<object> GetEvents(int gameId, Color color)
        // {
        //     return await m_gameService.GetGame(gameId).GetEventsAsync(color);
        // }
    }
}