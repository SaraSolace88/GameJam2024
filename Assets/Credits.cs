using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject CreditsScreen;

    public void On()
    {
        CreditsScreen.SetActive(true);
    }
    public void Off()
    {
        CreditsScreen?.SetActive(false);
    }
}
