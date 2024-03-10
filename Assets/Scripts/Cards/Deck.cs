using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck
{
    private List<Card> CardsInDeck { get; set; }

    // Constructor
    public Deck(List<Card> cardsInDeck)
    {
        CardsInDeck = cardsInDeck;
        ShuffleDeck();
    }
    
    // Fisher-Yates shuffling
    private void ShuffleDeck()
    {
        var deckSize = CardsInDeck.Count;
        while (deckSize > 1)
        {
            deckSize--;
            var next = new Random().Next(deckSize + 1);
            // Nice C# implementation of swapping values, no tmp needed
            (CardsInDeck[next], CardsInDeck[deckSize]) = (CardsInDeck[deckSize], CardsInDeck[next]);
        }
    }

    public Card DrawCard()
    {
        var topCard = CardsInDeck[0];
        CardsInDeck.RemoveAt(0);
        return topCard;
    }
}
