using System.Collections.Generic;
using Chess.Model;
using Chess.Model.Models;
using Chess.Server.Model;
using Piece = Chess.Model.Models.Piece;

namespace Chess.Server.Services
{
    public class LocalGameProviderService : IGameProviderService
    {
        private static readonly Dictionary<int, Game> m_gameState = new Dictionary<int, Game>
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
            }
        };

        public Game GetGame(int id)
        {
            return m_gameState[id];
        }

        public void StoreGame(int id, Game state)
        {
            m_gameState[id] = state;
        }
    }
}