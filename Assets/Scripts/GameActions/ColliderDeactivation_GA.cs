using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeactivation_GA : GameAction
{
    [SerializeField] private List<BoxCollider2D> colliders;

    public override void Action()
    {
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
