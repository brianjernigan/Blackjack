using System;
using System.Collections;
using UnityEngine;

public interface IPlayer
{
    bool IsActive { get; set; }
    Hand PlayerHand { get; set; }
    void Hit(Dealer dealer);
    void Stay();
}
