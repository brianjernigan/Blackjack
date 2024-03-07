using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Hand
{
    private const int BustingPoint = 21;
    public List<Card> Cards { get; set; } = new();
    public bool HasBusted => CalculateHand() > 21;
    public bool HasBlackjack => CalculateHand() == 21;

    public int CalculateHand()
    {
        return Cards.Sum(card => card.CardValue);
    }
}
