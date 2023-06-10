using System.Collections.ObjectModel;

namespace WPFMusicPlayer.Model
{
    public static class SongController
    {
        private static ObservableCollection<Song> _songs = new ObservableCollection<Song>();

        public static ObservableCollection<Song> Songs => _songs;
        public static Song AddSong { set => _songs.Add(value); }
    }
}