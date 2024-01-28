using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private List<Sprite> CharacterPoses;
    [SerializeField] private GameObject CharacterSprite;
    public void UpdatePose()
    {
        int I = Random.Range(0, CharacterPoses.Count);
        CharacterSprite.GetComponent<SpriteRenderer>().sprite = CharacterPoses[I];
    }
}

