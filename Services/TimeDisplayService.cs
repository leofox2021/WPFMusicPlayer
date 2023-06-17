namespace WPFMusicPlayer.Services;

public static class TimeDisplayService
{
    // Gets a "mm:ss"-formatted string position of the given minutes and seconds value
    // Returns "00:00" by deafult
    public static string ConvertToProperTimeFormat(int minutes = 0, int seconds = 0) =>
        (minutes < 10 ? $"0{minutes}" : $"{minutes}") + ":" + 
        (seconds < 10 ? $"0{seconds}" : $"{seconds}"); 
}