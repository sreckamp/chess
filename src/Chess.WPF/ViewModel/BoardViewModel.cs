using System.ComponentModel;
using System.Drawing;
using Chess.Model;
using GameBase.Model;
using GameBase.WPF.ViewModel;
using Piece = Chess.Model.Models.Piece;

namespace Chess.WPF.ViewModel
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        private readonly BoardStore m_board;
        private readonly ObservableList<PlacementViewModel> m_grid = new ObservableList<PlacementViewModel>();

        public BoardViewModel(BoardStore board)
        {
            m_board = board;
            // m_board.MinXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            // m_board.MaxXChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Columns));
            // m_board.MinYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            // m_board.MaxYChanged += (sender, args) =>  NotifyPropertyChanged(nameof(Rows));
            //TODO:var placements = new MappingCollection<PlacementViewModel, Placement<Piece>>(m_board.Placements);
            Floating = new ObservableList<PlacementViewModel>();
            Placements = new OverlayObservableList<PlacementViewModel>(m_grid, /*placements,*/ Floating);
            for (var y = 0; y < m_board.Size; y ++)
            {
                for (var x = 0; x < m_board.Size; x ++)
                {
                    if((x < board.CornerSize || x >= board.Size - board.CornerSize)
                        && (y < board.CornerSize || y >= board.Size - board.CornerSize)) continue;
                    
                    m_grid.Add(new PlacementViewModel(new Placement<Piece>(null, new Point(x,y))));
                }
            }
        }

        public OverlayObservableList<PlacementViewModel> Placements { get; }
        public IObservableList<PlacementViewModel> Floating { get; }

        public int Rows => m_board.Size;
        public int Columns => m_board.Size;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}