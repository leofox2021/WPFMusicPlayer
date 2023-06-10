using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;

namespace WPFMusicPlayer.Model
{
    public static class SongManager
    {
        private static ObservableCollection<Song> _songs = new ObservableCollection<Song>();

        public static ObservableCollection<Song> Songs => _songs;
        public static Song AddSong { set => _songs.Add(value); }
    }
}