using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFMusicPlayer.Command;
using WPFMusicPlayer.Model;

namespace WPFMusicPlayer.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Songs = SongController.Songs;
            PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
        }

        public ObservableCollection<Song> Songs { get; set; }
        public ICommand PlaySongCommand { get; set; }
        
        private void PlaySong(object obj)
        {
            // Playing logic will appear soon
        }

        private bool CanPlaySong(object obj) => true;
    }
}