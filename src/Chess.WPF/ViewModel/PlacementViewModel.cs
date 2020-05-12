using System;
using System.Text;
// using DPoint = System.Drawing.Point;
using System.Windows.Controls;
using System.Windows.Media;
using Chess.Model;
using GameBase.Model;
using GameBase.WPF.ViewModel;
using Piece = Chess.Model.Piece;

namespace Chess.WPF.ViewModel
{
    public class PlacementViewModel : AbstractPlacementViewModel<Piece, Move>
    {
        public PlacementViewModel(Piece piece) : this(new Placement<Piece, Move>(piece, null)) { }
        public PlacementViewModel(Placement<Piece, Move> placement) : base(placement) { }

        /// <inheritdoc />
        protected override Move GetMove(int locationX, int locationY)
        {
            return new Move(locationX, locationY);
        }

        public string Name => Placement.Piece.Name ?? string.Empty;
        public string Team => Placement.Piece.Team;

        public bool IsDark => (Column + Row) % 2 > 0;

        public string Location => $"({Placement.Move.Location})";

        private Brush m_lightColor = new SolidColorBrush(Colors.Tan);
        public Brush LightColor
        {
            get => m_lightColor;
            set
            {
                m_lightColor = value;
                NotifyPropertyChanged(nameof(LightColor));
            }
        }

        private Brush m_darkColor = new SolidColorBrush(Colors.SaddleBrown);
        public Brush DarkColor
        {
            get => m_darkColor;
            set
            {
                m_darkColor = value;
                NotifyPropertyChanged(nameof(DarkColor));
            }
        }
    }
}
