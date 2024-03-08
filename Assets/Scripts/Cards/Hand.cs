using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    private const int BustingPoint = 21;
    public List<Card> CardsInHand { get; set; } = new();
    public bool HasBusted => CalculateHand() > BustingPoint;
    public bool HasBlackjack => CalculateHand() == BustingPoint;

    private int CalculateHand()
    {
        if (!IsHoldingAce())
        {
            return CardsInHand.Sum(card => card.CardValue);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
    
    private bool IsHoldingAce()
    {
        return CardsInHand.Any(card => card.CardName.Contains("Ace"));
    }
}
