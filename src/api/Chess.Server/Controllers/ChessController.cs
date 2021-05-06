using Chess.Model;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Controllers
{
    [Route("chess/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameProviderService m_gameService;
        private readonly IGameTranslator m_translator;
        private static int s_gameId = 10000;

        public GamesController(IGameProviderService gameProvider, IGameTranslator translator)
        {
            m_gameService = gameProvider;
            m_translator = translator;
        }

        [HttpGet("{gameId?}")]
        public GameState GetGame(int? gameId, int players=2)
        {
            if (gameId == null)
            {
                gameId = s_gameId;
                s_gameId++;
                var g = new Game(players == 4 ? Version.FourPlayer : Version.TwoPlayer);
                m_gameService.StoreGame((int)gameId, g);
            }
            var id = (int) gameId;

            var game = m_gameService.GetGame(id);
            if (game.Store.Board == null)
            {
                game.Init();
            }

            return m_translator.FromModel(id, game.Store);
        }
    }
}