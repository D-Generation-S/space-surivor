using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class AudioManager : Node2D
{
    [Export(PropertyHint.Range, "1, 5")]
    private int startCount = 1;

    private List<AudioStreamPlayer2D> players;

    private Func<AudioStreamPlayer2D> onPlayerCreated;

    public AudioManager() : this(0, () => new AudioStreamPlayer2D())
    {
        
    }

    public AudioManager(int startCount, Func<AudioStreamPlayer2D> onPlayerCreated)
    {
        this.startCount = startCount;
        this.onPlayerCreated = onPlayerCreated;
    }

    public override void _Ready()
    {
        players = new List<AudioStreamPlayer2D>();
        for (int i = 0; i < startCount; i++)
        {
            players.Add(GenerateNewPlayer());
        }
        base._Ready();
    }

    public void QueueSoundToPlay(AudioStreamMP3 audioStreamMP3)
    {
        var player = GetFreePlayer();
        player.Stream = audioStreamMP3;
        player.Play();
    }

    public AudioStreamPlayer2D GetFreePlayer()
    {
        var freePlayer = players.Where(player => !player.Playing).FirstOrDefault();
        if (freePlayer is null)
        {
            freePlayer = GenerateNewPlayer();
            players.Add(freePlayer);
        }

        return freePlayer;
    }

    public AudioStreamPlayer2D GenerateNewPlayer()
    {
        var player = onPlayerCreated();
        AddChild(player);
        return player;
    }
}