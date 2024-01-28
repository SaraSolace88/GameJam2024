using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissingCollision : GameAction
{
    public NoteScore HitType;
    public static Action HitMiss = delegate { };
    public static Action<NoteScore> Hit = delegate { };
   
    public override void Action()
    {   
        Hit(HitType);
    }

}
