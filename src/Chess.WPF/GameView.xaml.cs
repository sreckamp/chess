using System.ComponentModel;
using System.Windows;
using Chess.WPF.ViewModel;

namespace Chess.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameView
    {
        private readonly GameViewModel m_gameViewModel;
        private readonly BackgroundWorker m_worker = new BackgroundWorker();

        public GameView()
        {
            InitializeComponent();
            DataContext = m_gameViewModel = new GameViewModel();
            m_worker.DoWork += m_worker_DoWork;
            m_worker.RunWorkerAsync();
        }

        private void m_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_gameViewModel.Run();
        }
    }
}