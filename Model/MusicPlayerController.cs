using System;
using System.Windows;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.Model;

public static class MusicPlayerController
{
    public static void OnNextSong()
    {
        // Prevent the app from crashing due to index > Songs.Count
        MainViewModel.Instance.SelectedSongIndex = 
            MainViewModel.Instance.SelectedSongIndex == MainViewModel.Instance.Songs.Count - 1 
                ? MainViewModel.Instance.SelectedSongIndex 
                : MainViewModel.Instance.SelectedSongIndex + 1;
            
        OnPlay();
    }
        
    public static void OnPreviousSong()
    {
        // Prevent the app from crashing due to index < 0
        MainViewModel.Instance.SelectedSongIndex = MainViewModel.Instance.SelectedSongIndex == 0 
            ? MainViewModel.Instance.SelectedSongIndex 
            : MainViewModel.Instance.SelectedSongIndex - 1;
            
        OnPlay();
    }
        
    public static void OnPlay()
    {
        try
        {
            MusicPlayer.Instance.Play(PlaylistController.Instance.Playlists[MainViewModel.Instance.SelectedPlaylistIndex]
                .Songs[MainViewModel.Instance.SelectedSongIndex]);
            MusicPlayer.Instance.Timer.Start();
            MainViewModel.Instance.Artwork = MusicPlayer.Instance.Song.Artwork;
            MainViewModel.Instance.Volume = MusicPlayer.Instance.Volume;    
        }
        catch (Exception) {} 
    }
        
    public static void OnPause()
    {
        MusicPlayer.Instance.Pause();
        MusicPlayer.Instance.Timer.Stop();
    }
        
    public static void OnStop()
    {
        MusicPlayer.Instance.Stop();
        MainViewModel.Instance.NaturalDuration = new Duration(TimeSpan.FromSeconds(0));
    }
        
    // Fires on tick of timer which is located in MusicPlayer class
    public static void UpdatePositionProperties(object sender, EventArgs e)
    {
        MainViewModel.Instance.PositionTimeSpan = MusicPlayer.Instance.Player.Position;
        MainViewModel.Instance.SliderPosition = MusicPlayer.Instance.Player.Position.TotalSeconds;
    }
        
    // Fires on musicplayer open event
    public static void UpdateDurationProperties(object sender, EventArgs e) =>
        MainViewModel.Instance.NaturalDuration = MusicPlayer.Instance.Player.NaturalDuration;
    
    public static void OnMediaEnded(object sender, EventArgs e)
    {
        if (MainViewModel.Instance.SelectedSongIndex < MainViewModel.Instance.Playlist.Songs.Count - 1) OnNextSong();
        else MusicPlayer.Instance.Stop();
    }
}