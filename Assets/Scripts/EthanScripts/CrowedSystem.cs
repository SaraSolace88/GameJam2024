using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrowedSystem : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private ParticleSystem CrowedPSHappy;
    [SerializeField] private ParticleSystem CrowedPSMid;
    [SerializeField] private ParticleSystem CrowedPSAngry;

    [SerializeField] private GameObject CrowedLevel2;
    [SerializeField] private GameObject CrowedLevel3;

    [SerializeField] private GameObject StartTarget;

    [SerializeField] private GameObject TargetC2;
    [SerializeField] private GameObject TargetC3;

    public bool C2UP;
    public bool C2DOWN;
    public bool C3UP;
    public bool C3DOWN;

    public bool Crowed2;
    public bool Crowed3;
    public void PlayEmotes(int CrowedHappyness)
    {
        Debug.Log("Playing");
        if(CrowedHappyness == 3)
        {
            CrowedPSHappy.Play();
        }
        if(CrowedHappyness == 2)
        {
            CrowedPSMid.Play();
        }
        if(CrowedHappyness == 1)
        {
            CrowedPSAngry.Play();
        }
    }
    private void Update()
    {
        if(Vector2.Distance(TargetC2.transform.position, CrowedLevel2.transform.position) <= 0.2f && C2UP)
        {
            C2UP = false;
            CrowedLevel2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (Vector2.Distance(StartTarget.transform.position, CrowedLevel2.transform.position) <= 0.2f && C2DOWN)
        {
            C2DOWN = false;
            CrowedLevel2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        if (Vector2.Distance(TargetC3.transform.position, CrowedLevel3.transform.position) <= 0.2f && C3UP)
        {
            C3UP = false;
            CrowedLevel3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (Vector2.Distance(StartTarget.transform.position, CrowedLevel3.transform.position) <= 0.2f && C3DOWN)
        {
            C3DOWN = false;
            CrowedLevel3.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if (C2UP)
        {
            Vector3 Direction = (TargetC2.transform.position - CrowedLevel2.transform.position).normalized;
            CrowedLevel2.GetComponent<Rigidbody2D>().velocity = Direction * 1.5f;
        }
        if (C2DOWN)
        {
            Vector3 Direction = (StartTarget.transform.position - CrowedLevel2.transform.position).normalized;
            CrowedLevel2.GetComponent<Rigidbody2D>().velocity = Direction * 1.5f;
        }
            if (C3UP)
            {
                Vector3 Direction = (TargetC3.transform.position - CrowedLevel3.transform.position).normalized;
                CrowedLevel3.GetComponent<Rigidbody2D>().velocity = Direction * 1.5f;
            }
            if (C3DOWN)
            {
                Vector3 Direction = (StartTarget.transform.position - CrowedLevel3.transform.position).normalized;
                CrowedLevel3.GetComponent<Rigidbody2D>().velocity = Direction * 1.5f;
            }
        
        else
        {
            C3UP = false;
            C3DOWN = false;
        }
    }

    public void MoveCrowedLevel2Up()
    {
        C2DOWN = false;
        C2UP = true;
        Crowed2 = true;
    }
    public void MoveCrowedLevel2Down()
    {
        C2UP = false;
        C2DOWN = true;
        Crowed2 = false;
    }

    public void MoveCrowedLevel3Up()
    {
            C3DOWN = false;
            C3UP = true;
            Crowed3 = true; 
    }
    public void MoveCrowedLevel3Down()
    {
            C3UP = false;
            C3DOWN = true;
            Crowed3 = false;
    }
}
