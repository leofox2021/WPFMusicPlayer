using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
        }
    }
}