using System.Collections.ObjectModel;
using System.Linq;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.Model
{
    public static class PlaylistController
    {
        public static ObservableCollection<Playlist> Playlists;
        
        public static void OnNextPlaylist()
        {
            MainViewModel.Instance.SelectedPlaylistIndex++;
        }
        
        public static void OnPreviousPlaylist()
        {
            MainViewModel.Instance.SelectedPlaylistIndex--;   
        }
        
        public static void OnAddPlaylist()
        {
            foreach (var playlistPath in FileDialogs.Playlist())
                Playlists.Add(new Playlist(playlistPath));
        }
        
        public static void OnCreateNewPlaylist()
        {
            if (!Playlists.Any(n => n.Name == ConstantStrings.DefaultPlaylist))
                Playlists.Add(new Playlist(ConstantStrings.DefaultPlaylistPath));
        }
    }
}