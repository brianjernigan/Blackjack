using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    public List<Card> Cards { get; set; } = new();

    public int CalculateHand()
    {
        var sum = 0;
        foreach (var card in Cards)
        {
            sum += card.CardValue;
        }

        return sum;
    }
}
