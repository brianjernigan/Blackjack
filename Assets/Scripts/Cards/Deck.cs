using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Deck
{
    private List<Card> _newDeck;
    public List<Card> NewDeck => _newDeck;

    // Constructor
    public Deck()
    {
        InitializeDeck();
        ShuffleDeck();
    }
    
    private void InitializeDeck()
    {
        _newDeck = new List<Card>();
        foreach (CardName cName in Enum.GetValues(typeof(CardName)))
        {
            foreach (CardSuit cSuit in Enum.GetValues(typeof(CardSuit)))
            {
                var newCard = new Card(cName, cSuit);
                _newDeck.Add(newCard);
            }
        }
    }

    private void ShuffleDeck()
    {
        var deckSize = _newDeck.Count;
        while (deckSize > 1)
        {
            deckSize--;
            var next = new Random().Next(deckSize + 1);
            (_newDeck[next], _newDeck[deckSize]) = (_newDeck[deckSize], _newDeck[next]);
        }
    }
}
