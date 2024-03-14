using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : Player
{
    private Deck GameDeck { get; }
    public Card FlippedCard { get; private set; }
    
    public Dealer(Deck gameDeck)
    {
        GameDeck = gameDeck;
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }

    public Card DealCard()
    {
        if (!GameDeck.CardsInDeck.Any())
        {
            throw new InvalidOperationException();
        }

        var topCard = GameDeck.CardsInDeck[0];
        GameDeck.CardsInDeck.RemoveAt(0);
        return topCard;
    }
    
    public void DealInitialHands(List<Player> activePlayers)
    {
        for (var i = 0; i < 2; i++)
        {
            foreach (var player in activePlayers)
            {
                player.Hit(this);
            }
        }

        // Hide dealer's first drawn card
        FlippedCard = PlayerHand.CardsInHand[0];
        FlippedCard.FlipCard();
    }
    
    public override string ToString()
    {
        return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
    }
}
