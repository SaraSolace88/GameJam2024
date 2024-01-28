using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private Transform spawnPos, removePos;
    private GameObject rhythmManager;
    private float beatOfThisNote;
    private int collum;
    private void Start()
    {
        rhythmManager = GameObject.FindWithTag("RhythmManager");

    }

    void Update()
    {
        if(spawnPos == null)
        {
            spawnPos = rhythmManager.transform.GetChild(collum-1).GetChild(0);
        }
        if (removePos == null)
        {
            removePos = rhythmManager.transform.GetChild(collum-1).GetChild(1);
        }

        if(spawnPos && removePos)
        {
            transform.position = Vector2.Lerp(
            spawnPos.position,
            removePos.position,
            (rhythmManager.GetComponent<RhythmSystem>().beatsShownInAdvance -
            (beatOfThisNote - rhythmManager.GetComponent<RhythmSystem>().songPosInBeats))
            / rhythmManager.GetComponent<RhythmSystem>().beatsShownInAdvance);
        }

        if(removePos.position == transform.position)
        {
            Destroy(gameObject);
        }
    }

    public void SetBOTN(float BOTN)
    {
        beatOfThisNote = BOTN;

    }

    public void SetCollum(int c)
    {
        collum = c;
    }
}
