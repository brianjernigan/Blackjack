using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    private List<Card> _hand;

    public int CalculateHand()
    {
        var sum = 0;
        foreach (var card in _hand)
        {
            sum += card._cardValue;
        }

        return sum;
    }

    public Hand()
    {
        _hand = new List<Card>();
    }
}
