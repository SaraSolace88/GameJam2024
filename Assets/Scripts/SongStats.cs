using System.Xml.Schema;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SongStats : MonoBehaviour
{
    public float TotalBeats;

    private PauseMenu PS;

    [SerializeField] private int Offset;

    //beats per minute of a song
    [SerializeField] private float bpm = 136;

    //keep all the position-in-beats of notes in the song and collum of track.
    //Vector2(position, collum)
    [SerializeField] private Vector2[] notes;
    [SerializeField] private Vector3[] heldNotes;


    //the index of the next note to be spawned
    private int nextIndex = 0, heldNextIndex = 0;

    [SerializeField] private GameObject musicNote, heldNote;

    [SerializeField] private Sprite MusicNote2, MusicNote3;
    [SerializeField] private Color color1, color2, color3;

    // Array of joke section start and end times in beats.
    /* [
        [4, 12] start on beat 4 end on beat 12 (inclusive)
        [20, 40]
    ]
    */
    [SerializeField] public Vector2[] joke_sections;
    private bool paused;

    private GameObject rhythmSystem;

    private void Awake()
    {
        //TotalBeats = notes[notes.Length-1].x;
    }

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
        if(nextIndex < notes.Length && notes[nextIndex].x + Offset < beats)
        {
            if(notes[nextIndex].y - 1 == 0)
            {
                GameObject tmp = Instantiate(musicNote);
                tmp.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y-1).GetChild(0).transform.position;
                tmp.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offset);
                tmp.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
            if(notes[nextIndex].y - 1 == 1)
            {
                GameObject tmp1 = Instantiate(musicNote);
                tmp1.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = MusicNote2;
                tmp1.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y-1).GetChild(0).transform.position;
                tmp1.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offset);
                tmp1.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
            if(notes[nextIndex].y - 1 == 2)
            {
                GameObject tmp2 = Instantiate(musicNote);
                tmp2.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = MusicNote3;
                tmp2.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)notes[nextIndex].y-1).GetChild(0).transform.position;
                tmp2.GetComponent<NoteMovement>().SetBOTN(notes[nextIndex].x+Offset);
                tmp2.GetComponent<NoteMovement>().SetCollum((int)notes[nextIndex].y-1);
                nextIndex++;
            }
        }

        if(heldNextIndex < heldNotes.Length && heldNotes[heldNextIndex].x + Offset < beats)
        {
            GameObject tmp = Instantiate(heldNote);
            if (heldNotes[nextIndex].z - 1 == 0)
            {
                tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color1;
            }
            if (heldNotes[nextIndex].z - 1 == 1)
            {
                tmp.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = MusicNote2;
                tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color2;
            }
            if (heldNotes[nextIndex].z - 1 == 2)
            {
                tmp.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = MusicNote3;
                tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color3;
            }
            tmp.transform.position = GameObject.FindWithTag("RhythmManager").transform.GetChild((int)heldNotes[heldNextIndex].z - 1).GetChild(0).transform.position;
            tmp.GetComponent<NoteMovement>().SetBOTN(heldNotes[heldNextIndex].x + Offset);
            tmp.GetComponent<NoteMovement>().SetCollum((int)heldNotes[heldNextIndex].z - 1);
            tmp.GetComponent<NoteMovement>().heldNote = true;
            tmp.GetComponent<NoteMovement>().beatOfTrail = heldNotes[heldNextIndex].y + Offset;
            tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(.5f, heldNotes[heldNextIndex].y);

            heldNextIndex++;
        }
    }
}
