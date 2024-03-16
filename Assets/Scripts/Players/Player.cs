using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    private const int TwentyOne = 21;
    public bool IsActive { get; set; }
    public Hand PlayerHand { get; set; } = new();
    
    public bool HasBusted => PlayerHand.HandScore > TwentyOne;
    public bool HasBlackjack => PlayerHand.HandScore == TwentyOne && PlayerHand.CardsInHand.Count == 2;
    public bool HasTwentyOne => PlayerHand.HandScore == TwentyOne;

    public abstract void Hit();
}
