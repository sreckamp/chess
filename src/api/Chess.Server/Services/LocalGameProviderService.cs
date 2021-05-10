using System.Collections.Generic;
using System.Linq;
using Chess.Model;
using Chess.Model.Models;
using Chess.Model.Stores;
using Piece = Chess.Model.Models.Piece;

namespace Chess.Server.Services
{
    public class LocalGameProviderService : IGameProviderService
    {
        private static int _gameId = 10000;
        private static readonly Dictionary<int, GameStore> GameStores = new Dictionary<int, GameStore>();

        static LocalGameProviderService()
        {
            GameStores[1] = Evaluator.Instance.Init(Version.FourPlayer, new GameBoard(5, 0)
            {
                [1, 0] = new Piece( PieceType.Knight, Color.Silver, Direction.West ),
                [3, 0] = new Piece( PieceType.Knight, Color.White, Direction.South ),
                [1, 4] = new Piece( PieceType.Knight, Color.Black, Direction.North ),
                [3, 4] = new Piece( PieceType.Knight, Color.Gold, Direction.East )
            });
            GameStores[2] = Evaluator.Instance.Init(Version.FourPlayer, new GameBoard(5, 0)
            {
                [2, 0] = new Piece(PieceType.King, Color.White, Direction.South),
                [2, 1] = new Piece(PieceType.Knight, Color.White, Direction.South),
                [2, 4] = new Piece(PieceType.Queen, Color.Black, Direction.North),
                [0, 2] = new Piece(PieceType.Queen, Color.Silver, Direction.West)
            });
            GameStores[3] = Evaluator.Instance.Init(Version.TwoPlayer, new GameBoard(5, 0)
            {
                [2, 4] = new Piece(PieceType.Queen, Color.Black, Direction.North),
                [3, 4] = new Piece(PieceType.Pawn, Color.Black, Direction.North),
                [2, 3] = new Piece(PieceType.King, Color.White, Direction.South)
            });
        }

        public GameStore GetGame(int id) => GameStores.ContainsKey(id) ? GameStores[id] : null;

        public IEnumerable<(int, GameStore)> ListGames() => GameStores.Select(pair => (pair.Key, pair.Value));

        public int CreateGame(Version version)
        {
            var id = _gameId;
            _gameId++;
            var g = Evaluator.Instance.Init(version);
            GameStores[id] = g;

            return id;
        }
    }
}