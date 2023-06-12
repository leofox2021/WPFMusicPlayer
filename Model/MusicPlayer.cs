using System;
using System.Windows.Media;
using System.Windows.Threading;
using WPFMusicPlayer.ViewModel;

namespace WPFMusicPlayer.Model
{
    // Music player is a singleton 
    public class MusicPlayer
    {
        private readonly MediaPlayer _player;
        private DispatcherTimer _timer;
        private static MusicPlayer _instance;
        
        private MusicPlayer()
        {
            _player = new MediaPlayer();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            _player.MediaOpened += PlayerOnMediaOpened;
            _player.MediaEnded += PlayerOnMediaEnded;
            _timer.Tick += TimerOnTick;
        }

        public static MusicPlayer Instance => _instance ?? (_instance = new MusicPlayer());
        
        public double Position
        {
            get => _player.Position.TotalSeconds;
            set
            {
                if (value != _player.Position.Seconds) 
                    _player.Position = TimeSpan.FromSeconds(value);   
            }
        }

        // Plays a new song
        public void Play(Song song = null)
        {
            // Check if the song is already opened
            if (song != null && _player.Source != new Uri(song.FullPath))
                _player.Open(new Uri(song.FullPath));
            
            _timer.Start();
            _player.Play();
        }

        public void Pause()
        {
            _player.Pause();
            _timer.Stop();
        }

        // Suspends playing a song
        public void Stop()
        {
            _player.Stop();
            _player.Close();

            MainViewModel.Instance.Duration = 0;
            MainViewModel.Instance.Timer = TimeSpanToString();
            MainViewModel.Instance.Length = TimeSpanToString();
        }
        
        // Timer tick event happens once in a second
        private void TimerOnTick(object sender, EventArgs e)
        {
            MainViewModel.Instance.Position = Position;
            MainViewModel.Instance.Timer = TimeSpanToString(_player.Position.Minutes, _player.Position.Seconds);
        }

        // Load duration for the UI
        private void PlayerOnMediaOpened(object sender, EventArgs e)
        {
            MainViewModel.Instance.Duration = _player.NaturalDuration.TimeSpan.TotalSeconds;
            MainViewModel.Instance.Length = TimeSpanToString(_player.NaturalDuration.TimeSpan.Minutes, 
                _player.NaturalDuration.TimeSpan.Seconds);
        }

        private void PlayerOnMediaEnded(object sender, EventArgs e) => Stop();
        
        // Gets a "mm:ss"-formatted string position of the given minutes and seconds value
        // Returns "00:00" by deafult
        private string TimeSpanToString(int minutes = 0, int seconds = 0) =>
            (minutes < 10 ? $"0{minutes}" : minutes.ToString()) + ":" + 
            (seconds < 10 ? $"0{seconds}" : seconds.ToString());
    }  
}