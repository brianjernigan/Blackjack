using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : Player
{
    private Deck GameDeck { get; }
    public Card HiddenCard { get; private set; }

    public override bool HasBlackjack
    {
        get
        {
            if (HiddenCard.IsHidden)
            {
                return false;
            }

            return PlayerHand.CalculateHandScore() == TwentyOne && NumCardsInHand == 2;
        }
    }

    public override bool HasTwentyOne
    {
        get
        {
            if (HiddenCard.IsHidden)
            {
                return false;
            }

            return PlayerHand.CalculateHandScore() == TwentyOne;
        }
    }

    public override int Score
    {
        get
        {
            if (HiddenCard.IsHidden)
            {
                return PlayerHand.CalculateHandScore() - HiddenCard.CardValue;
            }

            return PlayerHand.CalculateHandScore();
        }
    }

    // Constructor
    public Dealer(Deck gameDeck)
    {
        GameDeck = gameDeck;
    }

    public Card DrawCardFromDeck()
    {
        if (!GameDeck.CardsInDeck.Any())
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        var topCard = GameDeck.CardsInDeck[0];
        GameDeck.CardsInDeck.RemoveAt(0);
        return topCard;
    }

    public override void Hit()
    {
        var topCard = DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);
        
        if (NumCardsInHand == 1)
        {
            HiddenCard = topCard;
            FlipHiddenCard();
        }

        RaiseOnHit(topCard);
        CheckForBust();
        CheckForBlackjackOr21();
    }

    public void DealInitialHands(List<Player> activePlayers)
    {
        for (var i = 0; i < 2; i++)
        {
            foreach (var player in activePlayers)
            {
                player.Hit();
            }
        }
    }

    public void FlipHiddenCard()
    {
        HiddenCard.IsHidden = !HiddenCard.IsHidden;
    }
}