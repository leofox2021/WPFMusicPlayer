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
        private double _duration;
        private double _previousTickPosition;
        
        private MusicPlayer()
        {
            _player = new MediaPlayer();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _previousTickPosition = 0;

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
            
            _duration = 0;
        }
        
        private void TimerOnTick(object sender, EventArgs e)
        {
            MainViewModel.Instance.Position = Position;
            MainViewModel.Instance.Timer = GetStringPosition(_player.Position.Minutes, _player.Position.Seconds);
        }

        // Load duration for the UI
        private void PlayerOnMediaOpened(object sender, EventArgs e)
        {
            _duration = _player.NaturalDuration.TimeSpan.TotalSeconds;
            MainViewModel.Instance.Duration = _duration;
        }
        
        private void PlayerOnMediaEnded(object sender, EventArgs e) => Stop();
        
        private string GetStringPosition(int minutes, int seconds) =>
            (minutes < 10 ? $"0{minutes}" : minutes.ToString()) + ":" + 
            (seconds < 10 ? $"0{seconds}" : seconds.ToString());
    }  
}