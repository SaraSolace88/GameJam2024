using UnityEngine;

public class SongStats : MonoBehaviour
{
    private PauseMenu PS;
    [SerializeField] private int Offst;

    //beats per minute of a song
    [SerializeField] private float bpm = 136;

    //keep all the position-in-beats of notes in the song and collum of track.
    //Vector2(position, collum)
    [SerializeField] private Vector2[] notes;

    //the index of the next note to be spawned
    private int nextIndex = 0;

    [SerializeField] private GameObject musicNote;

    [SerializeField] private Sprite MusicNote2;
    [SerializeField] private Sprite MusicNote3;

    // Array of joke section start and end times in beats.
    /* [
        [4, 12] start on beat 4 end on beat 12 (inclusive)
        [20, 40]
    ]
    */
    [SerializeField] public Vector2[] joke_sections;
    private bool paused;

    private GameObject rhythmSystem;

    private void Start() {

        // Please don't fire me.
        PS = GameObject.Find("SceneLoaderObject(PutOneInEachScene)").GetComponent<PauseMenu>();
    }

    private void OnEnable()
    {
        RhythmSystem.SpawnNote += SpawnNote;
    }

    private void Update()
    {
        if (PS.IsPaused)
        {
            gameObject.GetComponent<AudioSource>().Pause();
            paused = true;
        }else if (paused)
        {
            gameObject.GetComponent<AudioSource>().Play();
            paused = false;
        }
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
        Debug.Log(gameObject);
        if(nextIndex < notes.Length && notes[nextIndex].x+Offst < beats)
        {
            if(notes[nextIndex].y - 1 == 0)
            {
                GameObject tmp = Instantiate(musicNote);
                tmp.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y).GetChild(0).transform.position;
                tmp.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offst);
                tmp.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
            if(notes[nextIndex].y - 1 == 1)
            {
                GameObject tmp1 = Instantiate(musicNote);
                tmp1.gameObject.GetComponent<SpriteRenderer>().sprite = MusicNote2;
                tmp1.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y).GetChild(0).transform.position;
                tmp1.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offst);
                tmp1.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
            if(notes[nextIndex].y - 1 == 2)
            {
                GameObject tmp2 = Instantiate(musicNote);
                tmp2.gameObject.GetComponent<SpriteRenderer>().sprite = MusicNote3;
                tmp2.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y).GetChild(0).transform.position;
                tmp2.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offst);
                tmp2.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
        }
    }
}
