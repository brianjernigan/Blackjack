using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Deck
{
    public List<Card> GameDeck { get; private set; }

    // Constructor
    public Deck()
    {
        InitializeDeck();
        ShuffleDeck();
    }
    
    private void InitializeDeck()
    {
        GameDeck = new List<Card>();
        foreach (CardName cName in Enum.GetValues(typeof(CardName)))
        {
            foreach (CardSuit cSuit in Enum.GetValues(typeof(CardSuit)))
            {
                var newCard = new Card(cName, cSuit);
                GameDeck.Add(newCard);
            }
        }
    }

    private void ShuffleDeck()
    {
        var deckSize = GameDeck.Count;
        while (deckSize > 1)
        {
            deckSize--;
            var next = new Random().Next(deckSize + 1);
            // Nice C# implementation of swapping values, no tmp needed
            (GameDeck[next], GameDeck[deckSize]) = (GameDeck[deckSize], GameDeck[next]);
        }
    }
}
