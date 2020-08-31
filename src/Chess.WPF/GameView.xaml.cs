using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
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
            m_gameViewModel = new GameViewModel();
            m_gameViewModel.Game.Init();
            InitializeComponent();
            // m_worker.DoWork += m_worker_DoWork;
            // m_worker.RunWorkerAsync();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = m_gameViewModel;

            // m_gameViewModel.Game.Start();
        }
        // private void m_worker_DoWork(object sender, DoWorkEventArgs e)
        // {
        //     m_gameViewModel.Run();
        // }
    }
}