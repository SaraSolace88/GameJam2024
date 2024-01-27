using System;
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
    private float dspTimeStart;
    //bool for if song is started or not
    [SerializeField] private bool songState, test;
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
        if (song.TryGetComponent<SongStats>(out SongStats stats))
        {
            secPerBeat = 60f / stats.GetBPM();
        }
    }

    private void Update()
    {
        if(test)
        {
            StartSong();

        }
        if(songState)
        {
            //calculate the position in seconds
            songPos = (float)(AudioSettings.dspTime - dspTimeStart);

            //calculate the position in beats
            songPosInBeats = songPos / secPerBeat;
            Debug.Log(songPosInBeats);
            SpawnNote(songPosInBeats + beatsShownInAdvance);
        }
    }

    private void StartSong()
    {
        test = false;
        songState = true;
        Debug.Log("before");
        //record the time when the song starts
        dspTimeStart = (float)AudioSettings.dspTime;
        Debug.Log("what what in the butt");
        //starts the song audio
        if (song.TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }
    }
}
