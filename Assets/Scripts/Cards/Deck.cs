using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Deck
{
    public List<Card> Cards { get; private set; }

    private void InitializeDeck()
    {
        Cards = new List<Card>();
        foreach (CardName cName in Enum.GetValues(typeof(CardName)))
        {
            foreach (CardSuit cSuit in Enum.GetValues(typeof(CardSuit)))
            {
                var newCard = new Card(cName, cSuit);
                Cards.Add(newCard);
            }
        }
    }

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
    
    // Constructor
    public Deck()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    public Card DrawCard()
    {
        if (!Cards.Any())
        {
            throw new IndexOutOfRangeException("Deck is empty");
        }

        var topCard = Cards[0];
        Cards.RemoveAt(0);
        return topCard;
    }

    public Hand InitialDeal()
    {
        var hand = new Hand();
        var initialHand = new List<Card>();
        for (var i = 0; i < 2; i++)
        {
            initialHand.Add(DrawCard());
        }

        hand.Cards = initialHand;
        return hand;
    }
}
