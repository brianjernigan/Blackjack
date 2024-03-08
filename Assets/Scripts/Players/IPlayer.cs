using System;
using System.Collections;
using UnityEngine;

public interface IPlayer
{
    Hand PlayerHand { get; set; }
    void Hit(Dealer dealer);
    void Stay();
}
