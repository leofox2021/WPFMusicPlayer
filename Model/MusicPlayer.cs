﻿using System;
using System.Windows.Media;
using System.Windows.Threading;
    
namespace WPFMusicPlayer.Model;

// Music player is a singleton 
public class MusicPlayer
{
    private readonly MediaPlayer _player;
    private DispatcherTimer _timer;
    private static MusicPlayer _instance;
    private Song _song;
        
    private MusicPlayer()
    {
        _player = new MediaPlayer();
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

        _player.MediaOpened += MusicPlayerController.UpdateDurationProperties;
        _player.MediaEnded += MusicPlayerController.OnMediaEnded;
        _timer.Tick += MusicPlayerController.UpdatePositionProperties;
    }

    public static MusicPlayer Instance => _instance ?? (_instance = new MusicPlayer());

    public MediaPlayer Player => _player;
    public Song Song => _song;
    public DispatcherTimer Timer => _timer;
        
    public double Position
    {
        get => _player.Position.TotalSeconds;
        set
        {
            if (value != _player.Position.TotalSeconds) 
                _player.Position = TimeSpan.FromSeconds(value);   
        }
    }
        
    public double Volume
    {
        get => _player.Volume;
        set
        {
            if (_player.Volume != value)
                _player.Volume = value;   
        }
    }

    // Plays a new song
    public void Play(Song song = null)
    {
        // Check if the song is already opened
        if (song != null && song != _song)
        {
            _song = song;
            _player.Open(new Uri(song.FullPath));
        }
            
        _player.Play();
    }

    public void Pause() => _player.Pause();

    // Suspends playing a song
    public void Stop(object sender = null, EventArgs e = null)
    {
        _player.Stop();
        _player.Close();
    }
}