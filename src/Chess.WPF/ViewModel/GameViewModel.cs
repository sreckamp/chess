using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;
using Chess.Model;
using GameBase.Model;
using GameBase.WPF.ViewModel;

namespace Chess.WPF.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly Game m_game = new Game(ChessVersion.FourPlayer);

        public GameViewModel()
        {
            BoardViewModel = new BoardViewModel(m_game.Board);
        }

        public BoardViewModel BoardViewModel { get; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        internal void Run()
        {
            while(true)
            {
                m_game.Play();
            }
        }
    }
}