using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomatoesGen : MonoBehaviour
{
    [SerializeField] private GameObject Tomatoe;
    [SerializeField] private int Amount;
    [SerializeField] private float delay;

    private bool Splated;
    private float timebetweensplats;

    private void Update()
    {
        timebetweensplats += Time.deltaTime;
        if (timebetweensplats >= 5f)
        {
            Splat();
            timebetweensplats = 0f;
        }
    }

    private void Splat()
    {
        Debug.Log("splat");

    }

    IEnumerator Unsplat()
    {
        yield return new WaitForSeconds(delay);
    }

}
