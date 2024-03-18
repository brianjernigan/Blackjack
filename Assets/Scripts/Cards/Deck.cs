using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck
{
    public List<Card> CardsInDeck { get; }

    // Constructor
    public Deck(List<Card> cardsInDeck)
    {
        CardsInDeck = cardsInDeck;
        ResetCardStates();
        ShuffleDeck();
    }

    private void ResetCardStates()
    {
        foreach (var card in CardsInDeck)
        {
            card.IsHidden = false;
        }
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
}
