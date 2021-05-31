using Chess.Server.Services.Model;

namespace Chess.Server.Model
{
    public interface IGameTranslator
    {
        GameState FromModel(Game game, bool includeMoves = false);

        Game ToModel(GameState state);
    }
}