using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private Transform spawnPos, removePos;
    private GameObject rhythmManager;
    private float beatOfThisNote;
    private void Start()
    {
        rhythmManager = GameObject.FindWithTag("RhythmManager");
    }

    void Update()
    {
        transform.position = Vector2.Lerp(
            spawnPos.position,
            removePos.position,
            (rhythmManager.GetComponent<RhythmSystem>().beatsShownInAdvance - (beatOfThisNote - rhythmManager.GetComponent<RhythmSystem>().songPosInBeats)) 
            / rhythmManager.GetComponent<RhythmSystem>().beatsShownInAdvance
        );
    }

    public void SetBOTN(float BOTN)
    {
        beatOfThisNote = BOTN;
    }
}
