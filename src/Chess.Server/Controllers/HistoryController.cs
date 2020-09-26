using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("chess/games/{gameId}/history")]
    public class HistoryController
    {
        private readonly IGameProviderService m_gameService;

        public HistoryController(IGameProviderService gameProvider)
        {
            m_gameService = gameProvider;
        }

    }
}