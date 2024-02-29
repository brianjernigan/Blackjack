using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NewDeck : MonoBehaviour
{
    [SerializeField] private List<SO_Card> _cards;

    public List<SO_Card> Cards
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
    
    private void Start()
    {
        ShuffleDeck();
        Debug.Log(Cards[0]);
        Debug.Log(Cards[0]._cardValue);
        Cards[0]._cardValue = 25;
        Debug.Log(Cards[0]._cardValue);
    }
}
