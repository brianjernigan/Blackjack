using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : IPlayer
{
    private Deck GameDeck { get; }
    public Card FlippedCard { get; private set; }
    public bool IsActive { get; set; }
    public Hand PlayerHand { get; set; } = new();
    
    public Dealer(Deck gameDeck)
    {
        GameDeck = gameDeck;
    }
    
    public void Hit(Dealer dealer)
    {
        // dealer = this
        DealCard(this);
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }

    private Card DrawCardFromDeck()
    {
        if (!GameDeck.CardsInDeck.Any())
        {
            throw new InvalidOperationException();
        }

        var topCard = GameDeck.CardsInDeck[0];
        GameDeck.CardsInDeck.RemoveAt(0);
        return topCard;
    }

    public void DealCard(IPlayer activePlayer)
    {
        var topCard = DrawCardFromDeck();
        activePlayer.PlayerHand.CardsInHand.Add(topCard);
    }
    
    public void DealInitialHands(List<IPlayer> activePlayers)
    {
        for (var i = 0; i < 2; i++)
        {
            foreach (var player in activePlayers)
            {
                DealCard(player);
            }
        }

        // Hide dealer's first drawn card
        FlippedCard = PlayerHand.CardsInHand[0];
        FlippedCard.FlipCard();
    }
}
