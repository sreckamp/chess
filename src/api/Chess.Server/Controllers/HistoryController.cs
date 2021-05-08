using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("api/games/{gameId}/history")]
    public class HistoryController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;

        public HistoryController(IGameProviderService gameProvider)
        {
            m_gameService = gameProvider;
        }

    }
}