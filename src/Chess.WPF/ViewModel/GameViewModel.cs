using System.ComponentModel;
using Chess.Model;
using Version = Chess.Model.Models.Version;

namespace Chess.WPF.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public GameViewModel()
        {
            BoardViewModel = new BoardViewModel(Game.Store.Board);
        }

        public BoardViewModel BoardViewModel { get; }

        public Game Game { get; } = new Game(Version.FourPlayer);

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        // internal void Run()
        // {
        //     while(true)
        //     {
        //         m_game.Play();
        //     }
        // }
    }
}