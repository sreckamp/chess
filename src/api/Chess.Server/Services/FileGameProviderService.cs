using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Services
{
    public class FileGameProviderService : IGameProviderService
    {
        private const string GAME_PATH = "game";
        public async Task<GameStore> GetGame(int id)
        {
            return (await ReadGame(id))?.Store;
        }

        public Task Update(int id, GameStore store)
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

        public Task<IEnumerable<Game>> ListGames()
        {
            throw new NotImplementedException();
            // lock (Games)
            // {
            //     return Games;
            // }
        }

        public Task<int> CreateGame(Version version, string name)
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

        private static async Task<Game> ReadGame(int gameId)
        {
            var path = GeneratePath(gameId);

            if (!File.Exists(path)) return default;
            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<Game>(stream);

        }

        private static async Task WriteGame(Game game)
        {
            var path = GeneratePath(game.Id);
            if (File.Exists(path))
            {
                await using var stream = File.OpenWrite(path);
                await JsonSerializer.SerializeAsync(stream, game);
            }
            
        }

        private static string GeneratePath(int id)
        {
            if (GAME_PATH.Length > 0)
            {
                Directory.CreateDirectory(GAME_PATH);
            }
            return $"{(GAME_PATH.Length > 0 ? $"{GAME_PATH}\\" : "")}{id}.chess.json";
        }
    }
}
