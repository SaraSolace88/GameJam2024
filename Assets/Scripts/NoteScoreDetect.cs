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
        string type = gameObject.tag;
        if(type == "Perfect")
        {
            HitType = NoteScore.Perfect;
        }else if (type == "Good")
        {
            HitType = NoteScore.Good;
        }
        else if (type == "Bad")
        {
            HitType = NoteScore.Bad;
        }
        Hit(HitType);
    }

}
