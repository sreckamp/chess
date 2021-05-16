using System.Collections.Generic;
using System.Linq;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private readonly IGameTranslator m_translator;

        public GamesController(IGameProviderService gameProvider, IGameTranslator translator)
        {
            m_gameService = gameProvider;
            m_translator = translator;
        }

        [HttpPost]
        public IActionResult Create([FromBody] GameSummary request)
        {
            var id = m_gameService.CreateGame(request.Players, request.Name);

            return Created(Url.RouteUrl("GetGame", new { id }), null);
        }

        [HttpGet]
        public IEnumerable<GameSummary> ListGame() => m_gameService.ListGames().Select(item => new GameSummary
        {
            Id = item.Id,
            Players = item.Store.Version
        });

        [HttpGet("{id:int}", Name = "GetGame")]
        public ActionResult<GameState> GetGame(int id)
        {
            var game = m_gameService.GetGame(id);

            if (game == null)
            {
                return BadRequest();
            }

            return m_translator.FromModel(id, game);
        }
    }
}