using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WPFMusicPlayer.Command;
using WPFMusicPlayer.Model;

namespace WPFMusicPlayer.ViewModel;

public partial class PlaylistNameViewModel : ObservableObject
{
    private static PlaylistNameViewModel _instance;

    [ObservableProperty] 
    private string _textBoxText;
    
    private PlaylistNameViewModel()
    {
        OkCommand = new RelayCommand(OnOkCommand, CanDoOkCommand);
        _textBoxText = "";
    }
    
    public static PlaylistNameViewModel Instance => _instance ??= new PlaylistNameViewModel();
    public ICommand OkCommand { get; set; }
    
    private bool CanDoOkCommand(object obj) => true;
    
    private void OnOkCommand(object obj) => 
        PlaylistController.Instance.OnSavePlaylist(MainViewModel.Instance.Songs, _textBoxText);
}