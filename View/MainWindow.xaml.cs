using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
        }
    }
}