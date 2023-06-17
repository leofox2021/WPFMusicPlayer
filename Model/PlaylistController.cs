using System.Collections.ObjectModel;
using WPFMusicPlayer.ViewModel;

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

        public static PlaylistController Instance => _instance ?? (_instance = new PlaylistController());
        public ObservableCollection<Playlist> Playlists { get; private set; }
        
        public void OnNextPlaylist() => MainViewModel.Instance.SelectedPlaylistIndex++;
        
        public void OnPreviousPlaylist() => MainViewModel.Instance.SelectedPlaylistIndex--;   

        public void OnAddPlaylist()
        {
            foreach (var playlistPath in FileDialogs.Playlist())
                Playlists.Add(new Playlist(playlistPath));
        }
        
        public void OnCreateNewPlaylist()
        {
            Playlists.Add(new Playlist(ConstantStrings.DefaultPlaylistPath));
        }
        
        public void OnSavePlaylist()
        {
            
        }
        
        public void OnAddSongsToPlaylist() =>
            Playlists[MainViewModel.Instance.SelectedPlaylistIndex].LoadSogns(FileDialogs.Song);
    }
}