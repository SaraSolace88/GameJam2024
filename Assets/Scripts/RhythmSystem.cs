using System;
using UnityEngine;

public class RhythmSystem : MonoBehaviour
{
    public PauseMenu PS; // Pause Menu Script


    //the current position of the song (in seconds)
    private float songPos;
    //the current position of the song (in beats)
    public float songPosInBeats;
    //the duration of a beat;
    private float secPerBeat;
    //how much time (in seconds) has passed since the song started
    private float dspTimeStart;
    //bool for if song is started or not
    [SerializeField] private bool songState, test;
    // Joke section we're on right now.
    private int joke_section_idx = 0;

    //amount of beats to show on track
    public int beatsShownInAdvance;

    //prefab called song that holds audio to play and songstats
    [SerializeField] private GameObject song;

    //check if next note should be spawned
    public static Action<float> SpawnNote = delegate { };

    // Tell everyone the song has started and the dsp's song start.
    public static Action<float> OnSongStart = delegate {};

    // Tell everyone that a new song section has started.
    // float : how long the section is in seconds.
    public static Action<float> OnJokeStart = delegate {};

    public static Action OnJokeEnd = delegate {};

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
        if (!PS.IsPaused)
        {
            if (test)
            {
                StartSong();

            }
            if (songState)
            {
                //calculate the position in seconds
                songPos = (float)(AudioSettings.dspTime - dspTimeStart);

                //calculate the position in beats
                float old_beat_pos = songPosInBeats;
                songPosInBeats = songPos / secPerBeat;
                SpawnNote(songPosInBeats + beatsShownInAdvance);

                if ((int)Math.Floor(old_beat_pos) != (int)Math.Floor(songPosInBeats))
                {
                    // new beat!
                    OnBeat(songPosInBeats);
                }

            }
        }
    }

    private void OnBeat(float time_in_beats) {

        // See if the start or end of a joke section has been reached.
        if (song.TryGetComponent<SongStats>(out SongStats stats))
        {
            if(stats.joke_sections.Length == 0) {
                return;
            }
            // This condition might not work if we skip enough frames to skip a whole beat.
            // to fix that, maybe just offset dsptimestart?
            if(stats.joke_sections[joke_section_idx][0] == (int)Math.Floor(time_in_beats)) {
                // Notify subscribers that a new joke section has started.
                float time_of_end_sec = secPerBeat * stats.joke_sections[joke_section_idx][1];
                float joke_length_sec = time_of_end_sec - secPerBeat*time_in_beats;
                OnJokeStart(joke_length_sec);
            }else if(stats.joke_sections[joke_section_idx][1] == (int)time_in_beats){
                OnJokeEnd();

                // Check if there are any joke sections left.
                if(stats.joke_sections.Length != joke_section_idx + 1) {
                    // There is one!
                    joke_section_idx += 1;
                }
            }
        }
    }

    private void StartSong()
    {
        test = false;
        songState = true;
        //record the time when the song starts
        dspTimeStart = (float)AudioSettings.dspTime;
        //starts the song audio
        if (song.TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }

        OnSongStart(dspTimeStart);
    }
}
