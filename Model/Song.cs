namespace WPFMusicPlayer.Model
{
    public class Song
    {
        public Song() {}
        
        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public uint TrackNumber { get; set; }
        public string FullPath { get; set; }
    }
}