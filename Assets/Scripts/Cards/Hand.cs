using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    private const int BustingPoint = 21;
    public List<Card> CardsInHand { get; set; } = new();
    public int HandScore => CalculateHandScore();
    public bool HasBusted => HandScore > BustingPoint;
    public bool HasBlackjack => HandScore == BustingPoint && CardsInHand.Count == 2;
    

    private int CalculateHandScore()
    {
        var handScore = CardsInHand.Sum(card => card.CardValue);
        var aceCount = CountAces();
        while (aceCount > 0 && handScore > BustingPoint)
        {
            handScore -= 10;
            aceCount--;
        }

        return handScore;
    }

    private int CountAces()
    {
        return CardsInHand.Count(card => card.CardName.Contains("Ace"));
    }
}
