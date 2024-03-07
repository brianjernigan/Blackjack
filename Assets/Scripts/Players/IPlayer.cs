using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    public Hand Hand { get; set; }
    public Deck Deck { get; set; }
    void Hit();
    void Stay();
}
