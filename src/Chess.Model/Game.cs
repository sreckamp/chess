using System.Drawing;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Reducers;
using Chess.Model.Stores;

namespace Chess.Model
{
    public class Game
    {
        private readonly GameReducer m_gameReducer = new GameReducer();
        public Version Version { get; }

        public Game(Version version)
        {
            Version = version;
            Store = new GameStore();
        }

        public GameStore Store { get; private set; }

        public void Init()
        {
            Store = m_gameReducer.Apply(new InitializeAction {Version = Version}, Store);
        }

        public void Move(Point from, Point to)
        {
            Store = m_gameReducer.Apply(new MoveAction {From = from, To = to}, Store);
        }
    }
}