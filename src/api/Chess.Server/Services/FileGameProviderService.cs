using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chess.Model;
using Chess.Model.Stores;
using Chess.Server.Services.Model;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Services
{
    public class FileGameProviderService : IGameProviderService
    {
        private const int MinGameId = 10000;
        private const string GamePath = "games";

        static FileGameProviderService()
        {
            if (GamePath.Length > 0)
            {
                Directory.CreateDirectory(GamePath);
            }
        }

        public async Task<GameStore> GetGame(int id)
        {
            return (await ReadGame(id))?.Store;
        }

        public async Task Update(int id, GameStore store)
        {
            if(store == default) return;
            
            var thisGame = await ReadGame(id);
        
            if (thisGame == default) return;
        
            thisGame.Store = store;

            await WriteGame(thisGame);
        }

        public async Task<IEnumerable<Game>> ListGames()
        {
            return await Task.WhenAll(ListGameIds().Select(async id => await ReadGame(id)));
        }

        public async Task<int> CreateGame(Version version, string name)
        {
            var game = new Game
            {
                Id = GetNextId(),
                Name = name,
                Store = Evaluator.Instance.Init(version)
            };

            var success = false;
            do
            {
                try
                {
                    await WriteGame(game);
                    success = true;
                }
                catch (IOException)
                {
                    game.Id++;
                }
            } while (!success);

            return game.Id;
        }

        private static IEnumerable<int> ListGameIds() =>
            Directory.GetFiles(GamePath.Length > 0 ? GamePath : ".", "*.chess.json")
                .Select(path => Regex.Match(path, @"(?:[^\]+\\)?([0-9]+)\.chess\.json"))
                .Where(match => match.Success)
                .Select(match => int.Parse(match.Groups[1].Value));

        private static async Task<Game> ReadGame(int gameId)
        {
            var path = GeneratePath(gameId);

            if (!File.Exists(path)) return default;
            await using var stream = File.OpenRead(path);
            return (await JsonSerializer.DeserializeAsync<GameFile>(stream)).ToModel();
        }

        private static async Task WriteGame(Game game)
        {
            await using var stream = File.OpenWrite(GeneratePath(game.Id));
            await JsonSerializer.SerializeAsync(stream, game.ToStorage());
        }

        private static string GeneratePath(int id)
        {
            return $"{(GamePath.Length > 0 ? $"{GamePath}\\" : "")}{id}.chess.json";
        }
        
        private static int GetNextId()
        {
            var maxId = ListGameIds().Append(MinGameId).Max();
            while (File.Exists(GeneratePath(maxId)))
            {
                maxId++;
            }

            return maxId;
        }
    }
}
