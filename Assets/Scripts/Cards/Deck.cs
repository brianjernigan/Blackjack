using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;

    [SerializeField] private GameObject _cardOnScreen;

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
    
    private void Start()
    {
        ShuffleDeck();
        var firstCard = Cards[0];
        _cardOnScreen.GetComponent<Image>().sprite = firstCard._cardSprite;
    }
}
