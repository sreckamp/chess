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
        public GameViewModel()
        {
            BoardViewModel = new BoardViewModel(Game.Board);
        }

        public BoardViewModel BoardViewModel { get; }

        public Game Game { get; } = new Game(ChessVersion.FourPlayer);

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