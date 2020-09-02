using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Http;
using Chess.Model;
using Chess.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Piece = Chess.Server.Models.Piece;

namespace Chess.Server.Controllers
{
    public class ChessController : ApiController
    {
        private static int m_game = 10000;
        private static readonly Dictionary<int, Game> m_gameState = new Dictionary<int, Game>();

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("chess/game/{game?}")]
        public BoardState GetGame(int? game)
        {
            if (game == null)
            {
                game = m_game;
                m_game++;
            }

            var id = (int) game;

            if (!m_gameState.ContainsKey(id))
            {
                var g = new Game(ChessVersion.FourPlayer);
                g.Init();
                m_gameState[id] = g;
            }

            return BuildResponse(id, m_gameState[id]);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("chess/game/{game}/moves/{color}")]
        public IEnumerable<Point> GetMoves([FromUri] int game, [FromUri] string color, [FromQuery] int x, [FromQuery] int y)
        {
            // if (!m_gameState.ContainsKey(game))
            // {
            //     return BadRequest("");
            // }
            return m_gameState[game].GetPossibleMoves(new Point(x, y));
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("chess/game/{game}/moves/{color}")]
        public IEnumerable<Point> PutMove([FromUri] int game, [FromUri] string color, [FromBody] Move m)
        {
            // if (!m_gameState.ContainsKey(game))
            // {
            //     return BadRequest("");
            // }
            return m_gameState[game].GetPossibleMoves(new Point(x, y));
        }

        private BoardState BuildResponse(int id, Game game)
        {
            return new BoardState
            {
                GameId = id,
                Name = $"Game {id}",
                Corners = game.Board.CornerSize,
                Width = game.Board.Width,
                Corner = game.Board.CornerSize % 2 == 1 ? "dark":"light",
                Other = game.Board.CornerSize % 2 == 1 ? "light":"dark",
                RotationMap = new Dictionary<string, string>()
                {
                    //TODO: generalize this
                    {"white", "none"},
                    {"silver", "counterclockwise"},
                    {"black", "upsidedown"},
                    {"gold", "clockwise"}
                },
                Pieces = game.Board.Placements.Select(placement => new Piece
                    {
                        X = placement.Location.X,
                        Y = placement.Location.Y,
                        Type = placement.Piece.Name,
                        Color = placement.Piece.Team
                    }) .ToArray()
            };
        }
    }
}