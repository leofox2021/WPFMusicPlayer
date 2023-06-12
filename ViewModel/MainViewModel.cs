using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPFMusicPlayer.Command;
using WPFMusicPlayer.Model;

namespace WPFMusicPlayer.ViewModel
{
    // MainViewModel is a singleton
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static MainViewModel _instance;
        private BitmapImage _artwork;
        private string _timer;
        private string _length;
        private int _selectedSongIndex;
        private double _duration;
        private double _position;
        private double _volume;
        
        private MainViewModel()
        {
            Songs = SongController.Songs;

            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
            PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
            PauseSongCommand = new RelayCommand(PauseSong, CanPauseSong);
            StopSongCommand = new RelayCommand(StopSong, CanStopSong);
            NextSongCommand = new RelayCommand(GoNextSong, CanGoNextSong);
            PreviousSongCommand = new RelayCommand(GoPreviousSong, CanGoPreviousSong);

            _timer = "00:00";
            _length = "00:00";
        }

        public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel()); 
        
        public string Timer
        {
            get => _timer;
            set
            {
                _timer = value;
                OnPropertyChanged();
            }
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
        
        public double Position
        {
            get => _position;
            set
            {
                _position = value > _duration ? _duration : value;
                MusicPlayer.Instance.Position = _position;
                OnPropertyChanged();
            }
        }

        public double Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }
        
        public string Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }
        
        public double Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                MusicPlayer.Instance.Volume = value;
                OnPropertyChanged();
            }
        }
        
        public BitmapImage Artwork
        {
            get => _artwork;
            set
            {
                _artwork = value;
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
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        private bool CanAddSong(object obj) => true;

        private void AddSong(object obj) => SongController.AddSong();

        private bool CanPlaySong(object obj) => true;
        
        private void PlaySong(object obj) => MusicPlayerController.OnPlay();
        
        private bool CanStopSong(object obj) => true;

        private void StopSong(object obj) => MusicPlayerController.OnStop();
        
        private bool CanGoPreviousSong(object obj) => true;

        private void GoPreviousSong(object obj) => MusicPlayerController.OnPreviousSong();

        private bool CanGoNextSong(object obj) => true;

        private void GoNextSong(object obj) => MusicPlayerController.OnNextSong();

        private bool CanPauseSong(object obj) => true;

        private void PauseSong(object obj) => MusicPlayerController.OnPause();
    }
}