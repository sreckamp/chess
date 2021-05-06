using Chess.Model;
using Chess.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Server.Controllers
{
    [Route("chess/evaluate")]
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
            var (id, store) = m_translator.ToModel(state);
            return m_translator.FromModel(id, Evaluator.Instance.Evaluate(store), true);
        }
    }
}
