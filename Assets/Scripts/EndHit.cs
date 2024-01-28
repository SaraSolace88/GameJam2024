using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndHit : MonoBehaviour
{
    public static Action HitMiss = delegate { };
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitMiss();
    }
}
