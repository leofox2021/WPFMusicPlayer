using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.View
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