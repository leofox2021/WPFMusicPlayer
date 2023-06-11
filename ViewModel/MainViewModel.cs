using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFMusicPlayer.Command;
using WPFMusicPlayer.Model;

namespace WPFMusicPlayer.ViewModel
{
    // MainViewModel is a singleton
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static MainViewModel _instance;
        private int _selectedSongIndex;
        private double _duration;
        private double _position;
        
        private MainViewModel()
        {
            Songs = SongController.Songs;

            AddSongCommand = new RelayCommand(AddSong, CanAddSong);
            PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
            PauseSongCommand = new RelayCommand(PauseSong, CanPauseSong);
            StopSongCommand = new RelayCommand(StopSong, CanStopSong);
            NextSongCommand = new RelayCommand(GoNextSong, CanGoNextSong);
            PreviousSongCommand = new RelayCommand(GoPreviousSong, CanGoPreviousSong);
        }

        public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel()); 
        
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

        private void PlaySong(object obj) => MusicPlayer.Instance.Play(Songs[_selectedSongIndex]);

        private bool CanPlaySong(object obj) => true;
        
        private void StopSong(object obj) => MusicPlayer.Instance.Stop();

        private bool CanStopSong(object obj) => true;
        
        private bool CanGoPreviousSong(object obj) => true;
        
        private void GoPreviousSong(object obj)
        {
            SelectedSongIndex--;
            MusicPlayer.Instance.Play(Songs[_selectedSongIndex--]);
        }

        private bool CanGoNextSong(object obj) => true;

        private void GoNextSong(object obj)
        {
            SelectedSongIndex++;
            MusicPlayer.Instance.Play(Songs[_selectedSongIndex]);
        }

        private bool CanPauseSong(object obj) => true;

        private void PauseSong(object obj) => MusicPlayer.Instance.Pause();
    }
}