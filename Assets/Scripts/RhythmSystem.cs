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

    //prefab called song that holds audio to play and songstats
    [SerializeField] private GameObject song;

    private void Start()
    {
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        if(song.TryGetComponent<SongStats>(out SongStats stats))
        {
            secPerBeat = 60f / stats.getBPM();
        }

        //record the time when the song starts
        dspTimeSong = (float) AudioSettings.dspTime;

        //start the song
        if(song.TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }
    }

    private void Update()
    {
        //calculate the position in seconds
        songPos = (float)(AudioSettings.dspTime - dspTimeSong);

        //calculate the position in beats
        songPosInBeats = songPos / secPerBeat;
    }
}
