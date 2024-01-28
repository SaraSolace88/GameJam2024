using System;
//using Unity.VisualScripting; // commented out to make a build put back if needed
//using UnityEditor.Experimental.GraphView; // commented out to make a build put back if needed
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmSystem : MonoBehaviour
{
    public PauseMenu PS;
    private Controls pInput;
    [SerializeField] private float timeAfterSong;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject BGRS;

    private ProgressBar pb;

    //the current position of the song (in seconds)
    private float songPos;
    //the current position of the song (in beats)
    public float songPosInBeats;
    //the duration of a beat;
    private float secPerBeat;
    //how much time (in seconds) has passed since the song started
    private float dspTimeStart;
    //bool for if song is started or not
    public bool songState;
    public bool test;
    // Joke section we're on right now.
    private int joke_section_idx = 0;

    //amount of beats to show on track
    public int beatsShownInAdvance;

    //prefab called song that holds audio to play and songstats
    private GameObject song;

    [SerializeField] private GameObject[] song_prefabs;

    //check if next note should be spawned
    public static Action<float> SpawnNote = delegate { };

    // Tell everyone the song has started and the dsp's song start.
    public static Action<float> OnSongStart = delegate {};

    // Tell everyone that a new song section has started.
    // float : how long the section is in seconds.
    public static Action<float> OnJokeStart = delegate {};

    public static Action OnJokeEnd = delegate {};

    private void Awake()
    {
        pb = GetComponent<ProgressBar>();
        int song_id = PlayerPrefs.GetInt("song_id");
        if(song_prefabs.Length == 0) {
            Debug.LogError("No songs have been added to RhythmSystem!");
        }
        song = Instantiate(song_prefabs[song_id]);
        
     
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        if (song.TryGetComponent<SongStats>(out SongStats stats))
        {
            secPerBeat = 60f / stats.GetBPM();
        }
    }

    private void OnEnable()
    {
        pInput = new Controls();
        pInput.Enable();
        pInput.Player.Start.performed += ButtonPressed;
    }
    private void OnDisable()
    {
        pInput.Player.Start.performed -= ButtonPressed;
    }

    private void ButtonPressed(InputAction.CallbackContext c)
    {
        if (!songState)
        {
            StartSong();
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
                if(pb.progressValue >= 1)
                {
                    Invoke("EndSong", timeAfterSong);
                    songState = false;
                }

                songPos = (PS.GetAdjustedAudioTime() - dspTimeStart);

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

    private void EndSong()
    {
        BGRS.SetActive(true);
        EndScreen.SetActive(true);
        PS.End();
    }

    private void StartSong()
    {
        test = false;
        songState = true;
        //record the time when the song starts
        dspTimeStart = PS.GetAdjustedAudioTime();
        //starts the song audio
        if (song.TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }

        OnSongStart(dspTimeStart);
    }
}