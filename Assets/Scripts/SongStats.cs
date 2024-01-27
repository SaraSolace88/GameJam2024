using Unity.VisualScripting;
using UnityEngine;

public class SongStats : MonoBehaviour
{


    //beats per minute of a song
    private float bpm = 136;

    //keep all the position-in-beats of notes in the song and collum of track.
    //Vector2(position, collum)
    [SerializeField] private Vector2[] notes;

    //the index of the next note to be spawned
    private int nextIndex = 0;

    [SerializeField] private GameObject musicNote;

    // Array of joke section start and end times in beats.
    /* [
        [4, 12] start on beat 4 end on beat 12 (inclusive)
        [20, 40]
    ]
    */
    [SerializeField] public Vector2[] joke_sections;

    private GameObject rhythmSystem;

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
            GameObject tmp = Instantiate(musicNote);
            tmp.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y).GetChild(0).transform.position;
            tmp.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x);
            tmp.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y);
            nextIndex++;
        }
    }
}
