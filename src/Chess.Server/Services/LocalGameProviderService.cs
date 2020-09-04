using System.Collections.Generic;
using Chess.Model;
using Chess.Server.Model;

namespace Chess.Server.Services
{
    public class LocalGameProviderService : IGameProviderService
    {
        private static readonly Dictionary<int, Game> m_gameState = new Dictionary<int, Game>();

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