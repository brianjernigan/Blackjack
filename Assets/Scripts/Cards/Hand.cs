using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    public List<Card> CardsInHand { get; set; } = new();
    public int HandScore => CalculateHandScore();

    private int CalculateHandScore()
    {
        var score = CardsInHand.Sum(card => card.CardValue);
        var elevenCount = CountAces();
        while (elevenCount > 0 && score > 21)
        {
            score -= 10;
            elevenCount--;
        }

        return score;
    }

    private int CountAces()
    {
        return CardsInHand.Count(card => card.CardName.Contains("Ace"));
    }

    public void AddCardToHand(Card cardToAdd)
    {
        CardsInHand.Add(cardToAdd);
    }
}
