using System;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.Model
{
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
            MusicPlayer.Instance.Play(PlaylistController.Instance.Playlists[MainViewModel.Instance.SelectedPlaylistIndex]
                .Songs[MainViewModel.Instance.SelectedSongIndex]);
            MusicPlayer.Instance.Timer.Start();
            MainViewModel.Instance.Artwork = MusicPlayer.Instance.Song.Artwork;
            MainViewModel.Instance.Volume = MusicPlayer.Instance.Volume;
        }
        
        public static void OnPause()
        {
            MusicPlayer.Instance.Pause();
            MusicPlayer.Instance.Timer.Stop();
        }
        
        public static void OnStop()
        {
            MusicPlayer.Instance.Stop();
            MainViewModel.Instance.Duration = 0;
            MainViewModel.Instance.Timer = TimeSpanToString();
            MainViewModel.Instance.Length = TimeSpanToString();
        }
        
        // Fires on tick of timer which is located in MusicPlayer class
        public static void UpdatePositionProperties(object sender, EventArgs e)
        {
            MainViewModel.Instance.Position = MusicPlayer.Instance.Position;
            MainViewModel.Instance.Timer = TimeSpanToString(MusicPlayer.Instance.MinutesNow, MusicPlayer.Instance.SecondsNow);
        }
        
        // Fires on musicplayer open event
        public static void UpdateDurationProperties(object sender, EventArgs e)
        {
            MainViewModel.Instance.Duration = MusicPlayer.Instance.TotalSeconds;
            MainViewModel.Instance.Length = 
                TimeSpanToString(MusicPlayer.Instance.MinutesOverall, MusicPlayer.Instance.SecondsOverall);
        }
        
        // Gets a "mm:ss"-formatted string position of the given minutes and seconds value
        // Returns "00:00" by deafult
        private static string TimeSpanToString(int minutes = 0, int seconds = 0) =>
            (minutes < 10 ? $"0{minutes}" : minutes.ToString()) + ":" + 
            (seconds < 10 ? $"0{seconds}" : seconds.ToString());
    }
}