using System.Collections.ObjectModel;
using System.IO;
using WPFMusicPlayer.ViewModel;
using System.Windows.Forms;

namespace WPFMusicPlayer.Model
{
    public class PlaylistController
    {
        private static PlaylistController _instance;
        
        private PlaylistController()
        {
            Playlists = new ObservableCollection<Playlist>();
            OnCreateNewPlaylist();   
        }

        public static PlaylistController Instance => _instance ??= new PlaylistController();
        public ObservableCollection<Playlist> Playlists { get; private set; }
        
        public void OnNextPlaylist() => MainViewModel.Instance.SelectedPlaylistIndex++;
        
        public void OnPreviousPlaylist() => MainViewModel.Instance.SelectedPlaylistIndex--;   

        public void OnAddPlaylist()
        {
            foreach (var playlistPath in FileDialogs.Playlist())
                Playlists.Add(new Playlist(playlistPath));
        }
        
        public void OnCreateNewPlaylist() =>
            Playlists.Add(new Playlist(ConstantStrings.DefaultPlaylistPath));
        
        public void OnSavePlaylist(ObservableCollection<Song> songs, string name)
        {
            var records = "";
            var playlistsDirectory = Directory.GetCurrentDirectory() + @"\playlists\"; 
            var filePath = playlistsDirectory + name + ".m3u";
            
            foreach (var song in songs)
                records += $"{song.FullPath}\n";

            if (!Directory.Exists(playlistsDirectory))
                Directory.CreateDirectory(playlistsDirectory);
                    
            using (var file = File.Create(filePath)) {}
            File.WriteAllText(filePath, records);
        }
        
        public void OnAddSongsToPlaylist() =>
            Playlists[MainViewModel.Instance.SelectedPlaylistIndex].LoadSogns(FileDialogs.Song);
    }
}