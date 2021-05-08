using System.Collections.Generic;
using System.Linq;
using Chess.Model;
using Chess.Model.Models;
using Piece = Chess.Model.Models.Piece;

namespace Chess.Server.Services
{
    public class LocalGameProviderService : IGameProviderService
    {
        private static int s_gameId = 10000;
        private static readonly Dictionary<int, Game> GameState = new Dictionary<int, Game>
        {
            {1, new Game(Version.FourPlayer, new GameBoard(5, 0)
                {
                    [1, 0] = new Piece( PieceType.Knight, Color.Silver, Direction.West ),
                    [3, 0] = new Piece( PieceType.Knight, Color.White, Direction.South ),
                    [1, 4] = new Piece( PieceType.Knight, Color.Black, Direction.North ),
                    [3, 4] = new Piece( PieceType.Knight, Color.Gold, Direction.East )
                })
            },
            {2, new Game(Version.FourPlayer, new GameBoard(5, 0)
                {
                    [2, 0] = new Piece( PieceType.King, Color.White, Direction.South ),
                    [2, 1] = new Piece( PieceType.Knight, Color.White, Direction.South ),
                    [2, 4] = new Piece( PieceType.Queen, Color.Black, Direction.North ),
                    [0, 2] = new Piece( PieceType.Queen, Color.Silver, Direction.West )
                })
            },
            {3, new Game(Version.TwoPlayer, new GameBoard(5, 0)
            {
                [2, 4] = new Piece( PieceType.Queen, Color.Black, Direction.North ),
                [3, 4] = new Piece( PieceType.Pawn, Color.Black, Direction.North ),
                [2, 3] = new Piece( PieceType.King, Color.White, Direction.South )
            })
        }
        };

        public Game GetGame(int id) => GameState[id];

        public IEnumerable<(int, Game)> ListGames() => GameState.Select(pair => (pair.Key, pair.Value));

        public int CreateGame(Version version)
        {
            var id = s_gameId;
            s_gameId++;
            var g = new Game(version);
            GameState[id] = g;

            return id;
        }
    }
}