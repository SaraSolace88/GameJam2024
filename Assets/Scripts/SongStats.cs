using UnityEngine;

public class SongStats : MonoBehaviour
{


    //beats per minute of a song
    private float bpm = 8;

    //keep all the position-in-beats of notes in the song and collum of track.
    //Vector2(position, collum)
    private Vector2[] notes;

    //the index of the next note to be spawned
    private int nextIndex = 0;

    public float getBPM()
    {
        return bpm;
    }
}
