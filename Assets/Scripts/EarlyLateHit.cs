using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EarlyLateHit : GameAction
{
    public static Action Late = delegate { };
    public override void Action()
    {
        Late();
    }
}
