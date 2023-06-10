using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WPFMusicPlayer.Model
{
    public static class MusicPlayer
    {
        private static MediaPlayer _player = new MediaPlayer();

        // Plays a new song
        public static void Play(Song song = null)
        {
            // Check if the song is already opened
            if (song != null && _player.Source != new Uri(song.FullPath))
                _player.Open(new Uri(song.FullPath));
            
            _player.Play();
        }

        public static void Pause() => _player.Pause();

        // Suspends playing a song
        public static void Stop()
        {
            _player.Stop();
            _player.Close();
        }
    }
}