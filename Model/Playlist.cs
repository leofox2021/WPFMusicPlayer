using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using TagLib;
using File = System.IO.File;
using PathManager = System.IO.Path;

namespace WPFMusicPlayer.Model
{
    public class Playlist
    {
        public Playlist(string path)
        {
            if (!path.Equals("default")) LoadSongsFromM3U(path); Path = "default";
            Path = path;
            Name = path.Equals("default") ? "Default Playlist" : PathManager.GetFileName(path);
        }
        
        public ObservableCollection<Song> Songs { get; set; }
        public string Path;
        public string Name;
        public int SongQuantity => Songs.Count;
        
        public void LoadSogns(string[] paths)
        {
            foreach (var path in paths)
                LoadSong(path);
        }
        
        private void LoadSongsFromM3U(string path)
        {
            foreach (var line in File.ReadAllLines(path))
                LoadSong(line);
        } 

        private BitmapImage GetAlbumArtwork(IPicture picture)
        {
            var bitmap = new BitmapImage();
            
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(picture.Data.Data);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
        
        private void LoadSong(string path)
        {
            var musicFile = TagLib.File.Create(path);
                
            Songs.Add(new Song
            {
                Name = musicFile.Tag.Title,
                Album = musicFile.Tag.Album,
                Artist = musicFile.Tag.FirstAlbumArtist,
                TrackNumber = musicFile.Tag.Track,
                Duration = musicFile.Length,
                FullPath = path,
                Artwork = GetAlbumArtwork(musicFile.Tag.Pictures[0])
            });
        }
    }
}