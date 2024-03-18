//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using System.Collections.Generic;
using Random = System.Random;

public class Deck
{
    // Constructor
    public Deck(List<Card> cardsInDeck)
    {
        CardsInDeck = cardsInDeck;
        ResetCardStates();
        ShuffleDeck();
    }

    public List<Card> CardsInDeck { get; }

    // Ensure cards are not hidden on load
    private void ResetCardStates()
    {
        foreach (var card in CardsInDeck) card.IsHidden = false;
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