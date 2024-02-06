using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// A simple audio manager which will spawn 2d stream audio players as required
/// This class will allow playing sounds in parallel
/// </summary>
public partial class AudioManager : Node2D
{
    /// <summary>
    /// The number of audio players to start with
    /// </summary>
    [Export(PropertyHint.Range, "1, 5")]
    private int startCount = 1;

    /// <summary>
    /// All players currently attached to this manager
    /// </summary>
    private List<AudioStreamPlayer2D> players;

    /// <summary>
    /// Method to create new audio players
    /// </summary>
    private Func<AudioStreamPlayer2D> onPlayerCreated;

    /// <summary>
    /// Create a new instance of this object,
    /// with 0 default players and a normal player instantiation
    /// </summary>
    public AudioManager() : this(0, () => new AudioStreamPlayer2D())
    {
        
    }

    /// <summary>
    /// Create a new instance of this object
    /// </summary>
    /// <param name="startCount">The number of players to start with</param>
    /// <param name="onPlayerCreated">The method used to create audio players</param>
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

    /// <summary>
    /// Queue a new sound to be played
    /// </summary>
    /// <param name="audioStreamMP3">The mp3 file to play</param>
    public void QueueSoundToPlay(AudioStreamMP3 audioStreamMP3)
    {
        var player = GetFreePlayer();
        player.Stream = audioStreamMP3;
        player.Play();
    }

    /// <summary>
    /// Get the next free player or create a new one if required
    /// </summary>
    /// <returns>A useable audio stream player</returns>
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

    /// <summary>
    /// Generate a new player if required
    /// </summary>
    /// <returns>The new generated player</returns>
    public AudioStreamPlayer2D GenerateNewPlayer()
    {
        var player = onPlayerCreated();
        AddChild(player);
        return player;
    }
}