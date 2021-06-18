using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Model;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;
using Piece = Chess.Model.Models.Piece;

namespace Chess.Server.Services
{
    public class MemoryGameProviderService : IGameProviderService
    {
        private static int s_gameId = 10000;
        private static readonly IList<Game> Games;

        static MemoryGameProviderService() =>
            Games = new List<Game>
            {
                new Game
                {
                    Id = 1,
                    Name = "Knight Test",
                    Store = Evaluator.Instance.Init(Version.FourPlayer, new GameBoard(5, 0)
                    {
                        [1, 0] = new Piece( PieceType.Knight, Color.Silver, Direction.West ),
                        [3, 0] = new Piece( PieceType.Knight, Color.White, Direction.South ),
                        [1, 4] = new Piece( PieceType.Knight, Color.Black, Direction.North ),
                        [3, 4] = new Piece( PieceType.Knight, Color.Gold, Direction.East )
                    })
                },
                new Game
                {
                    Id = 2,
                    Name = "Pin Test",
                    Store = Evaluator.Instance.Init(Version.FourPlayer, new GameBoard(5, 0)
                    {
                        [2, 0] = new Piece(PieceType.King, Color.White, Direction.South),
                        [2, 1] = new Piece(PieceType.Knight, Color.White, Direction.South),
                        [2, 4] = new Piece(PieceType.Queen, Color.Black, Direction.North),
                        [0, 2] = new Piece(PieceType.Queen, Color.Silver, Direction.West)
                    })
                },
                new Game
                {
                    Id = 3,
                    Name = "King In Check Twice",
                    Store = Evaluator.Instance.Init(Version.TwoPlayer, new GameBoard(5, 0)
                    {
                        [2, 4] = new Piece(PieceType.Queen, Color.Black, Direction.North),
                        [3, 4] = new Piece(PieceType.Pawn, Color.Black, Direction.North),
                        [2, 3] = new Piece(PieceType.King, Color.White, Direction.South)
                    })
                }
            };

        public Task<Game> GetGame(int id)
        {
            lock (Games)
            {
                return Task.FromResult(Games.FirstOrDefault(game => game.Id == id));
            }
        }

        public Task<Game> UpdateStore(int id, GameStore store)
        {
            lock (Games)
            {
                var game = Games.FirstOrDefault(g => g.Id == id);

                if(store == default || game == default) return Task.FromResult(game);

                game.Store = store;

                return Task.FromResult(game);
            }
        }

        public Task<IEnumerable<Game>> ListGames()
        {
            lock (Games)
            {
                return Task.FromResult(Games as IEnumerable<Game>);
            }
        }

        public Task<int> CreateGame(Version version, string name)
        {
            lock (Games)
            {
                var game = new Game
                {
                    Id = s_gameId++,
                    Name = name,
                    Store = Evaluator.Instance.Init(version)
                };

                Games.Add(game);

                return Task.FromResult(game.Id);
            }
        }

        public Color ColorForConnectionId(int id, string mConnectionId)
        {
            return Color.None;
        }
    }
}