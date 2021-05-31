using Chess.Model;
using Chess.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("api/evaluate")]
    public class EvaluationController
    {
        private readonly IGameTranslator m_translator;

        public EvaluationController(IGameTranslator translator)
        {
            m_translator = translator;
        }

        [HttpPost]
        public GameState EvaluateBoard([FromBody] GameState state)
        {
            var game = m_translator.ToModel(state);
            game.Store = Evaluator.Instance.Evaluate(game.Store);
            return m_translator.FromModel(game, true);
        }
    }
}
