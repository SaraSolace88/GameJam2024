using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay;
    void Start()
    {
        var rand = new System.Random();
        double x = rand.NextDouble() * (6.2 - 3.9) + 3.9;
        double y = rand.NextDouble() * (-2 - 4.5) + 4.5;
        gameObject.transform.position = new Vector3((float)x,(float)y,-4);
        StartCoroutine(nameof(Unsplat));
    }

    IEnumerator Unsplat()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
