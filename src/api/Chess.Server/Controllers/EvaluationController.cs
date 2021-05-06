using System;
using Chess.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chess.Server.Controllers
{
    [Route("chess/evaluate")]
    public class EvaluationController
    {
        [HttpPost]
        public GameState EvaluateBoard([FromBody] GameState game)
        {
            Console.WriteLine($"Evaluate: {game}");
            return game;
        }
        
        [HttpPost("raw")]
        public GameState EvaluateRawBoard([FromBody] dynamic game)
        {
            Console.WriteLine($"Evaluate: {game}");
            return null;
        }
    }
}
