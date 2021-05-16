namespace Chess.Server.Services.Model
{
    public static class GameExtensions
    {
        public static GameFile ToStorage(this Game game) => new GameFile
        {
            
        };

        public static Game ToModel(this GameFile file) => new Game
        {

        };
    }
}