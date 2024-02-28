using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardName
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}

public enum CardSuit
{
    Hearts,
    Diamonds,
    Spades,
    Clubs
}

public class Card
{
    public CardName Name { get; }
    public CardSuit Suit { get; }
    public int CardValue { get; set; }

    private readonly Dictionary<CardName, int> _cardValues = new()
    {
        // Default Ace to 1, will change depending on scenario
        { CardName.Ace, 1 },
        { CardName.Two, 2 },
        { CardName.Three, 3 },
        { CardName.Four, 4 },
        { CardName.Five, 5 },
        { CardName.Six, 6 },
        { CardName.Seven, 7 },
        { CardName.Eight, 8 },
        { CardName.Nine, 9 },
        { CardName.Ten, 10 },
        { CardName.Jack, 10 },
        { CardName.Queen, 10 },
        { CardName.King, 10 }
    };
    
    public Card(CardName name, CardSuit suit)
    {
        Name = name;
        Suit = suit;
        CardValue = _cardValues[name];
    }

    // For debugging
    public override string ToString() => $"{Name} of {Suit}";
}
