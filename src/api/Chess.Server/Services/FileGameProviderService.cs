using System;
using System.Collections.Generic;
using Chess.Model.Stores;
using Chess.Server.Services.Model;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Services
{
    public class FileGameProviderService : IGameProviderService
    {
        public GameStore GetGame(int id)
        {
            throw new NotImplementedException();
            // lock (Games)
            // {
            //     return Games.FirstOrDefault(game => game.Id == id)?.Store;
            // }
        }

        public void Update(int id, GameStore store)
        {
            throw new NotImplementedException();
            // if(store == default) return;
            //
            // lock (Games)
            // {
            //     var thisGame = Games.FirstOrDefault(g => g.Id == id);
            //
            //     if (thisGame == default) return;
            //
            //     thisGame.Store = store;
            // }
        }

        public IEnumerable<Game> ListGames()
        {
            throw new NotImplementedException();
            // lock (Games)
            // {
            //     return Games;
            // }
        }

        public int CreateGame(Version version, string name)
        {
            throw new NotImplementedException();
            // lock (Games)
            // {
            //     var game = new Game
            //     {
            //         Id = s_gameId++,
            //         Name = name,
            //         Store = Evaluator.Instance.Init(version)
            //     };
            //
            //     Games.Add(game);
            //
            //     return game.Id;
            // }
        }
    }
}
