using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    public List<Card> Cards { get; set; } = new();

    public int CalculateHand()
    {
        return Cards.Sum(card => card.CardValue);
    }

    public void AddCard(Card cardToAdd)
    {
        Cards.Add(cardToAdd);
    }
}
