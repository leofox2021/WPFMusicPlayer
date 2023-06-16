using System.Windows.Forms;

namespace WPFMusicPlayer.Model
{
    public static class FileDialogs
    {
        public static string[] Playlist() => Dialog("Playlists (*.m3u)|*.m3u");
        public static string[] Song => Dialog("MP3 files (*.mp3)|*.mp3");
        
        private static string[] Dialog(string filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = true
            };

            return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileNames : new string[] { };
        }
    }
}