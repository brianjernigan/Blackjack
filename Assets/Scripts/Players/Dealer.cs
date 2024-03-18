//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

public class Dealer : Player
{
    // Constructor
    public Dealer(Deck gameDeck)
    {
        GameDeck = gameDeck;
    }

    // Only Dealer can interact with the deck
    private Deck GameDeck { get; }
    public Card HiddenCard { get; private set; }

    public override bool HasBlackjack
    {
        get
        {
            // "Hide" blackjack if still player's turn
            if (HiddenCard.IsHidden) return false;

            return PlayerHand.CalculateHandScore() == TwentyOne && NumCardsInHand == 2;
        }
    }

    public override bool HasTwentyOne
    {
        get
        {
            // "Hide" 21 if still player's turn
            if (HiddenCard.IsHidden) return false;

            return PlayerHand.CalculateHandScore() == TwentyOne;
        }
    }

    public override int Score
    {
        get
        {
            // Adjust score if still player's turn
            if (HiddenCard.IsHidden) return PlayerHand.CalculateHandScore() - HiddenCard.CardValue;

            return PlayerHand.CalculateHandScore();
        }
    }

    public Card DrawCardFromDeck()
    {
        if (!GameDeck.CardsInDeck.Any()) throw new InvalidOperationException("The deck is empty.");

        var topCard = GameDeck.CardsInDeck[0];
        GameDeck.CardsInDeck.RemoveAt(0);
        return topCard;
    }

    public override void Hit()
    {
        var topCard = DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);

        // Set Hidden Card on initial card
        if (NumCardsInHand == 1)
        {
            HiddenCard = topCard;
            FlipHiddenCard();
        }

        RaiseOnHit(topCard);
        // Avoid multiple stays before turn
        if (!IsActive) return;
        CheckForBust();
        CheckForBlackjackOr21();
    }

    public void DealInitialHands(List<Player> activePlayers)
    {
        for (var i = 0; i < 2; i++)
            foreach (var player in activePlayers)
                player.Hit();
    }

    public void FlipHiddenCard()
    {
        HiddenCard.IsHidden = !HiddenCard.IsHidden;
    }
}