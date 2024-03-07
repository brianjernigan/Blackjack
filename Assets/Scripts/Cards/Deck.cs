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

    public Card DrawCard()
    {
        if (!_cards.Any())
        {
            throw new IndexOutOfRangeException();
        }

        var firstCard = _cards[0];
        _cards.RemoveAt(0);
        return firstCard;
    }
    
    private void ShuffleDeck()
    {
        var deckSize = _cards.Count;
        while (deckSize > 1)
        {
            deckSize--;
            var next = new Random().Next(deckSize + 1);
            // Nice C# implementation of swapping values, no tmp needed
            (_cards[next], _cards[deckSize]) = (_cards[deckSize], _cards[next]);
        }
    }
}
