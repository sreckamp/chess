using System.ComponentModel;
using System.Drawing;
using Chess.Model.Stores;
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
            //TODO:var placements = new MappingCollection<PlacementViewModel, Placement<Piece>>(m_board.Placements);
            Floating = new ObservableList<PlacementViewModel>();
            Placements = new OverlayObservableList<PlacementViewModel>(m_grid, /*placements,*/ Floating);
            for (var y = 0; y < m_board.Board.Size; y ++)
            {
                for (var x = 0; x < m_board.Board.Size; x ++)
                {
                    if(!board.Board.IsOnBoard(x,y)) continue;
                    
                    m_grid.Add(new PlacementViewModel(new Placement<Piece>(null, new Point(x,y))));
                }
            }
        }

        public OverlayObservableList<PlacementViewModel> Placements { get; }
        public IObservableList<PlacementViewModel> Floating { get; }

        public int Rows => m_board.Board.Size;
        public int Columns => m_board.Board.Size;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}