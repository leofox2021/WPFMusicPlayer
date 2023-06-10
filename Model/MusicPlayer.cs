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

        public static void Play() => _player.Play();

        public static void Pause() => _player.Pause();

        public static void Stop() => _player.Stop();
    }
}