using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RhythmSystem : MonoBehaviour
{
    //the current position of the song (in seconds)
    private float songPos;
    //the current position of the song (in beats)
    private float songPosInBeats;
    //the duration of a beat;
    private float secPerBeat;
    //how much time (in seconds) has passed since the song started
    private float dspTimeSong;
    //bool for if song is started or not
    private bool songState;
    //amount of beats to show on track
    [SerializeField] private int beatsShownInAdvance;

    //prefab called song that holds audio to play and songstats
    [SerializeField] private GameObject song;

    //check if next note should be spawned
    public static Action<float> SpawnNote = delegate { };

    private void Start()
    {
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        if(song.TryGetComponent<SongStats>(out SongStats stats))
        {
            secPerBeat = 60f / stats.GetBPM();
        }

        //record the time when the song starts
        dspTimeSong = (float) AudioSettings.dspTime;
    }

    private void Update()
    {
        if(songState)
        {
            //calculate the position in seconds
            songPos = (float)(AudioSettings.dspTime - dspTimeSong);

            //calculate the position in beats
            songPosInBeats = songPos / secPerBeat;

            SpawnNote(songPosInBeats + beatsShownInAdvance);
        }
    }

    private void StartSong()
    {
        songState = true;

        //starts the song audio
        if (song.TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }
    }
}
