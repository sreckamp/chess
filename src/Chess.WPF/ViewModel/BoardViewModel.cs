using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using Chess.Model;
using GameBase.Model;
using GameBase.WPF.ViewModel;
using Piece = Chess.Model.Piece;

namespace Chess.WPF.ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        private readonly Board m_board;

        public BoardViewModel(Board board)
        {
            m_board = board;
            m_board.MinXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            m_board.MaxXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            m_board.MinYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            m_board.MaxYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            Placements = new DispatchedMappingCollection<PlacementViewModel, Placement<Piece,Move>>(m_board.Placements);
            Floating = new ObservableList<PlacementViewModel>();
        }

        public IObservableList<PlacementViewModel> Placements { get; }
        public IObservableList<PlacementViewModel> Floating { get; }

        public int Rows => m_board.Height;
        public int Columns => m_board.Width;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}