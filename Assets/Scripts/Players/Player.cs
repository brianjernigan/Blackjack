using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Hand Hand { get; set; }

    protected Player()
    {
        Hand = new Hand();
    }
}
