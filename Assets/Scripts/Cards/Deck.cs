using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<Card> InitializeDeck()
    {
        var freshDeck = new List<Card>();
        foreach (CardName cName in Enum.GetValues(typeof(CardName)))
        {
            foreach (CardSuit cSuit in Enum.GetValues(typeof(CardSuit)))
            {
                var newCard = new Card(cName, cSuit);
                freshDeck.Add(newCard);
            }
        }
        return freshDeck;
    }
}
