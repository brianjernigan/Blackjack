using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardName
{
    Ace,
    One,
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
    private CardName _cardName;
    private CardSuit _cardSuit;

    public CardName CardName
    {
        get => _cardName;
        set => _cardName = value;
    }

    public CardSuit CardSuit
    {
        get => _cardSuit;
        set => _cardSuit = value;
    }

    public int CardValue
    {
        get
        {
            return _cardName switch
            {
                CardName.One => 1,
                CardName.Two => 2,
                CardName.Three => 3,
                CardName.Four => 4,
                CardName.Five => 5,
                CardName.Six => 6,
                CardName.Seven => 7,
                CardName.Eight => 8,
                CardName.Nine => 9,
                CardName.Ten => 10,
                CardName.Jack => 10,
                CardName.Queen => 10,
                CardName.King => 10,
                _ => 0,
            };
        }
        set{}
    }

    public Card(CardName cardName, CardSuit cardSuit)
    {
        _cardName = cardName;
        _cardSuit = cardSuit;
    }

    public override string ToString() => $"{CardName} of {CardSuit}";
}
