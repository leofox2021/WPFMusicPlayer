using System.Windows;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.View;

public partial class PlaylistNameDialog : Window
{
    public PlaylistNameDialog()
    {
        InitializeComponent();
        DataContext = PlaylistNameViewModel.Instance;
    }
}