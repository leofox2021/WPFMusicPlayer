using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPFMusicPlayer.Model;
using WPFMusicPlayer.Services;
using RelayCommand = WPFMusicPlayer.Command.RelayCommand;

namespace WPFMusicPlayer.ViewModel;

// MainViewModel is a singleton
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Duration))]
    [NotifyPropertyChangedFor(nameof(Length))]
    private Duration _naturalDuration;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Timer))]
    [NotifyPropertyChangedFor(nameof(SliderPosition))]
    private TimeSpan _positionTimeSpan;

    [ObservableProperty]
    private BitmapImage _artwork;
        
    [ObservableProperty]
    private int _selectedSongIndex;
        
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Songs))]
    [NotifyPropertyChangedFor(nameof(Playlist))]
    private int _selectedPlaylistIndex;
        
    private static MainViewModel _instance;
    
    private double _position;
    private double _volume;
        
    private MainViewModel()
    {
        Playlists = PlaylistController.Instance.Playlists;
            
        AddSongCommand = new RelayCommand(AddSong, CanAddSong);
            
        PlaySongCommand = new RelayCommand(PlaySong, CanPlaySong);
        PauseSongCommand = new RelayCommand(PauseSong, CanPauseSong);
        StopSongCommand = new RelayCommand(StopSong, CanStopSong);
        NextSongCommand = new RelayCommand(GoNextSong, CanGoNextSong);
        PreviousSongCommand = new RelayCommand(GoPreviousSong, CanGoPreviousSong);
            
        NewPlaylistCommand = new RelayCommand(CreateNewPlaylist, CanCreateNewPlaylist);
        AddPlaylistCommand = new RelayCommand(AddPlaylist, CanAddPlaylist);
        SavePlaylistCommand = new RelayCommand(SavePlaylist, CanSavePlaylist);
            
        NextPlaylistCommand = new RelayCommand(GoNextPlaylist, CanGoNextPlaylist);
        PreviousPlaylistCommand = new RelayCommand(GoPreviousPlaylist, CanGoPreviousPlaylist);
    }

    public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel());

    public string Timer =>
        TimeDisplayService.ConvertToProperTimeFormat(_positionTimeSpan.Minutes, _positionTimeSpan.Seconds);

    public double Duration => _naturalDuration.HasTimeSpan ? _naturalDuration.TimeSpan.TotalSeconds : 0;

    public string Length => TimeDisplayService.ConvertToProperTimeFormat
    (
        _naturalDuration.HasTimeSpan ? _naturalDuration.TimeSpan.Minutes : 0,
        _naturalDuration.HasTimeSpan ? _naturalDuration.TimeSpan.Seconds : 0
    );
        
    public double SliderPosition
    {
        get => _position;
        set
        {
            _position = value;
            MusicPlayer.Instance.Position = value;
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

    public Playlist Playlist => Playlists[_selectedPlaylistIndex];
    public ObservableCollection<Song> Songs => Playlists[_selectedPlaylistIndex].Songs;
    public ObservableCollection<Playlist> Playlists { get; set; }

    public ICommand AddSongCommand { get; set; }
    public ICommand PlaySongCommand { get; set; }
    public ICommand PauseSongCommand { get; set; }
    public ICommand StopSongCommand { get; set; }
    public ICommand NextSongCommand { get; set; }
    public ICommand PreviousSongCommand { get; set; }
    public ICommand NewPlaylistCommand { get; set; }
    public ICommand AddPlaylistCommand { get; set; }
    public ICommand SavePlaylistCommand { get; set; }
    public ICommand NextPlaylistCommand { get; set; }
    public ICommand PreviousPlaylistCommand { get; set; }
        
    private bool CanAddSong(object obj) => true;

    private void AddSong(object obj) => PlaylistController.Instance.OnAddSongsToPlaylist();  

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
        
    private bool CanGoPreviousPlaylist(object obj) => true;

    private void GoPreviousPlaylist(object obj) => PlaylistController.Instance.OnPreviousPlaylist();

    private bool CanGoNextPlaylist(object obj) => true;

    private void GoNextPlaylist(object obj) => PlaylistController.Instance.OnNextPlaylist();

    private bool CanAddPlaylist(object obj) => true;

    private void AddPlaylist(object obj) => PlaylistController.Instance.OnAddPlaylist();
        
    private bool CanSavePlaylist(object obj) => true;

    private void SavePlaylist(object obj) => PlaylistController.Instance.OnSavePlaylist();

    private bool CanCreateNewPlaylist(object obj) => true;

    private void CreateNewPlaylist(object obj) => PlaylistController.Instance.OnCreateNewPlaylist();
}