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
            Path = path;
            Name = path.Equals(ConstantStrings.DefaultPlaylistPath) 
                ? ConstantStrings.DefaultPlaylist 
                : PathManager.GetFileName(path);
            
            if (!path.Equals("default"))
                LoadSongsFromM3U();
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
        
        public void LoadSongsFromM3U()
        {
            foreach (var line in File.ReadAllLines(Path))
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