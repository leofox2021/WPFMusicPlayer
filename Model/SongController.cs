using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace WPFMusicPlayer.Model
{
    public static class SongController
    {
        private static ObservableCollection<Song> _songs = new ObservableCollection<Song>();

        public static ObservableCollection<Song> Songs => _songs;
        
        public static void AddSong()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*",
                Multiselect = true
            };
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    var musicFile = TagLib.File.Create(file);
                 
                    _songs.Add(new Song
                    {
                        Name = musicFile.Tag.Title,
                        Album = musicFile.Tag.Album,
                        Artist = musicFile.Tag.FirstAlbumArtist,
                        TrackNumber = musicFile.Tag.Track,
                        Duration = musicFile.Length,
                        FullPath = file,
                        // Artwork = GetAlbumArtwork(musicFile.Tag.Pictures[0])
                    });
                }
            }
        }
    }
}