namespace WPFMusicPlayer.Model
{
    public class Song
    {
        public Song(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
        }
        
        public string Name { get; set; }
        public string FullPath { get; set; }
    }
}