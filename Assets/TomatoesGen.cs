using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomatoesGen : MonoBehaviour
{
    [SerializeField] private GameObject Tomatoe;
    [SerializeField] private int Amount;
    [SerializeField] private float delay;

    Scoring score_sys;

    private bool Splated;
    private float timebetweensplats = 0;

    public void Start() {
        score_sys = gameObject.GetComponent<Scoring>();
    }

    private void Update()
    {
        if(score_sys.Grade == 'D') {
            timebetweensplats += Time.deltaTime;
            if (timebetweensplats >= 5f)
            {
                Splat();
                timebetweensplats = 0f;
            }
        }
    }

    private void Splat()
    {
        Debug.Log("splat");
        //Set Random Location on the Chart
        //X (3.9 - 6.2)
        //Y (-2 - 4.5)
        var ob = Instantiate(Tomatoe);
        var clus = ob.GetComponent<TomatoScript>();
        clus.delay = delay;
    }

}
