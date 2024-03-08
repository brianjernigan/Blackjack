using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck
{
    private List<Card> Cards { get; set; }

    // Constructor
    public Deck(List<Card> cards)
    {
        Cards = cards;
        ShuffleDeck();
    }
    
    // Fisher-Yates shuffling
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

    public void DrawCard(IPlayer activePlayer)
    {
        var topCard = Cards[0];
        Cards.RemoveAt(0);
        activePlayer.PlayerHand.Cards.Add(topCard);
    }
}
