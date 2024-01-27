using UnityEngine;

public class SongStats : MonoBehaviour
{


    //beats per minute of a song
    private float bpm = 136;

    //keep all the position-in-beats of notes in the song and collum of track.
    //Vector2(position, collum)
    private Vector2[] notes;

    //the index of the next note to be spawned
    private int nextIndex = 0;

    [SerializeField] private GameObject musicNote;

    private void OnEnable()
    {
        RhythmSystem.SpawnNote += SpawnNote;
    }

    private void OnDisable()
    {
        RhythmSystem.SpawnNote -= SpawnNote;
    }
    public float GetBPM()
    {
        return bpm;
    }

    private void SpawnNote(float beats)
    {
        if(nextIndex < notes.Length && notes[nextIndex].x < beats)
        {
            Instantiate(musicNote);
        }
    }
}
