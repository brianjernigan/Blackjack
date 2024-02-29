using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    public List<Card> Cards { get; set; }

    public int CalculateHand()
    {
        return Cards.Sum(card => card.CardValue);
    }
}
