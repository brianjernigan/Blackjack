using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck
{
    public List<Card> Cards { get; set; }

    // Constructor
    public Deck(List<Card> cards)
    {
        Cards = cards;
        ShuffleDeck();
    }
    
    // Fisher-Yates algorithm
    private void ShuffleDeck()
    {
        var deckSize = Cards.Count;
        while (deckSize > 1)
        {
            deckSize--;
            var next = new Random().Next(deckSize + 1);
            // Nice C# implementation of swapping values, no tmp needed
            (Cards[next], Cards[deckSize]) = (Cards[deckSize], Cards[next]);
        }
    }

    public Card DrawCard()
    {
        var topCard = Cards[0];
        Cards.RemoveAt(0);
        return topCard;
    }
}
