using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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
        private readonly ObservableList<PlacementViewModel> m_grid = new ObservableList<PlacementViewModel>();

        public BoardViewModel(Board board)
        {
            m_board = board;
            m_board.MinXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            m_board.MaxXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            m_board.MinYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            m_board.MaxYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            var placements = new MappingCollection<PlacementViewModel, Placement<Piece>>(m_board.Placements);
            Floating = new ObservableList<PlacementViewModel>();
            Placements = new OverlayObservableList<PlacementViewModel>(m_grid, placements, Floating);
            for (var y = 0; y < m_board.Height; y ++)
            {
                for (var x = 0; x < m_board.Width; x ++)
                {
                    if((x < board.CornerSize || x >= board.Width - board.CornerSize)
                        && (y < board.CornerSize || y >= board.Height - board.CornerSize)) continue;
                    
                    m_grid.Add(new PlacementViewModel(new Placement<Piece>(null, new Point(x,y))));
                }
            }
        }

        public OverlayObservableList<PlacementViewModel> Placements { get; }
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