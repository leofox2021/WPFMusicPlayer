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
            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
        }

        private bool CanAddSong(object obj)
        {
            return true;
        }

        private void AddSong(object obj)
        {
            SongController.AddSong();
        }

        public ObservableCollection<Song> Songs { get; set; }
        
        public ICommand AddSongCommand { get; set; }
        public ICommand PlaySongCommand { get; set; }
        public ICommand PauseSongCommand { get; set; }
        public ICommand StopSongCommand { get; set; }
        public ICommand NextSongCommand { get; set; }
        public ICommand PreviousSongCommand { get; set; }
        
        private void PlaySong(object obj)
        {
            
        }

        private bool CanPlaySong(object obj) => true;
    }
}