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

        public async Task<Game> GetGame(int id) => await ReadGame(id);

        public async Task<Game> UpdateStore(int id, GameStore store)
        {
            var game = await GetGame(id);

            if(store == default || game == default) return game;

            game.Store = store;

            await WriteGame(game);

            return game;
        }

        public async Task<IEnumerable<Game>> ListGames() =>
            await Task.WhenAll(ListGameIds().Select(async id => await ReadGame(id)));

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
                .Select(path => Regex.Match(path, @"\\([0-9]+)\.chess\.json"))
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
            await using var stream = File.Create(GeneratePath(game.Id));
            await JsonSerializer.SerializeAsync(stream, game.ToStorage());
        }

        private static string GeneratePath(int id) =>
            $"{(GamePath.Length > 0 ? $"{GamePath}\\" : "")}{id}.chess.json";
        
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
