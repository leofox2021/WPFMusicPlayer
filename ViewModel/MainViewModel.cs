using System.Collections.ObjectModel;
using System.ComponentModel;
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

            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
            PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
            PauseSongCommand = new RelayCommand(PauseSong, CanPauseSong);
            StopSongCommand = new RelayCommand(StopSong, CanStopSong);
            NextSongCommand = new RelayCommand(GoNextSong, CanGoNextSong);
            PreviousSongCommand = new RelayCommand(GoPreviousSong, CanGoPreviousSong);
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

        private void PlaySong(object obj) => MusicPlayer.Play(Songs[_selectedSongIndex]);

        private bool CanPlaySong(object obj) => true;
        
        private void StopSong(object obj) => MusicPlayer.Stop();

        private bool CanStopSong(object obj) => true;
        
        private bool CanGoPreviousSong(object obj) => true;
        
        private void GoPreviousSong(object obj)
        {
            SelectedSongIndex -= 1;
            MusicPlayer.Play(Songs[_selectedSongIndex]);
        }

        private bool CanGoNextSong(object obj) => true;

        private void GoNextSong(object obj)
        {
            SelectedSongIndex += 1;
            MusicPlayer.Play(Songs[_selectedSongIndex]);
        }

        private bool CanPauseSong(object obj) => true;

        private void PauseSong(object obj) => MusicPlayer.Pause();
    }
}