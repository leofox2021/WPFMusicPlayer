using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using WPFMusicPlayer.Command;
using WPFMusicPlayer.Model;

namespace WPFMusicPlayer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _selectedSongIndex;
        
        public MainViewModel()
        {
            Songs = SongController.Songs;
            
            PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
        }

        public int SelectedSongIndex
        {
            get => _selectedSongIndex; 
            set
            {
                _selectedSongIndex = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<Song> Songs { get; set; }
        
        public ICommand AddSongCommand { get; set; }
        public ICommand PlaySongCommand { get; set; }
        public ICommand PauseSongCommand { get; set; }
        public ICommand StopSongCommand { get; set; }
        public ICommand NextSongCommand { get; set; }
        public ICommand PreviousSongCommand { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private bool CanAddSong(object obj) => true;

        private void AddSong(object obj) => SongController.AddSong();

        private void PlaySong(object obj) => MusicPlayer.Play();

        private bool CanPlaySong(object obj) => true;
    }
}