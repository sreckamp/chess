using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public class AwsElasticacheGameProviderService : IGameProviderService
    {
        public GameStore GetGame(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Game> ListGames()
        {
            throw new System.NotImplementedException();
        }

        public int CreateGame(Version version, string name)
        {
            throw new System.NotImplementedException();
        }
        
        public void Update(int id, GameStore store)
        {
            throw new System.NotImplementedException();
        }
    }
}