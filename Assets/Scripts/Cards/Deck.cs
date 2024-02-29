using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    public List<Card> Cards
    {
        get => _cards;
        set => _cards = value;
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

    private Card DrawCard()
    {
        if (!Cards.Any())
        {
            throw new IndexOutOfRangeException();
        }

        var firstCard = Cards[0];
        Cards.RemoveAt(0);
        return firstCard;
    }
    
    private void Start()
    {
        ShuffleDeck();
    }
}
