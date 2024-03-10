using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : IPlayer
{
    public bool IsActive { get; set; }
    private Deck GameDeck { get; set; }
    public Hand PlayerHand { get; set; } = new();
    public Dealer(Deck gameGameDeck)
    {
        GameDeck = gameGameDeck;
    }

    public void Hit(Dealer dealer)
    {
        dealer.DealCard(this);
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }
    
    public void DealCard(IPlayer activePlayer)
    {
        var topCard = GameDeck.DrawCard();
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
        FlipDealerCard(PlayerHand.CardsInHand[0]);
    }

    private void FlipDealerCard(Card cardToFlip)
    {
        cardToFlip.IsHidden = !cardToFlip.IsHidden;
    }
    
    public override string ToString()
    {
        return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
    }
}
