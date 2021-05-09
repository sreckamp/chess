using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Stores;

namespace Chess.Server.Services
{
    public class AwsElasticacheGameProviderService : IGameProviderService
    {
        public GameStore GetGame(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<(int, GameStore)> ListGames()
        {
            throw new System.NotImplementedException();
        }

        public int CreateGame(Version version)
        {
            throw new System.NotImplementedException();
        }
    }
}