using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : Player
{
    private Deck GameDeck { get; }
    public Card HiddenCard { get; private set; }

    // Constructor
    public Dealer(Deck gameDeck, GameController gameController)
    {
        IsActive = false;
        GameDeck = gameDeck;
        GameController = gameController;
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
            GameController._dealerHiddenCard = GameController.SpawnCard(PlayerHand.CardsInHand[NumCardsInHand - 1], NumCardsInHand - 1, this);
        }
        else
        {
            GameController.SpawnCard(PlayerHand.CardsInHand[NumCardsInHand - 1], NumCardsInHand - 1, this);
        }
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