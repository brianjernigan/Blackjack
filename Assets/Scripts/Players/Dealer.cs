using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : IPlayer
{
    public Deck Deck { get; set; }
    public Hand PlayerHand { get; set; } = new();
    public Dealer(Deck gameDeck)
    {
        Deck = gameDeck;
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
        Deck.DrawCard(activePlayer);
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
        FlipDealerCard(PlayerHand.Cards[0]);
    }

    private void FlipDealerCard(Card cardToFlip)
    {
        cardToFlip.IsHidden = !cardToFlip.IsHidden;
    }
    
    public override string ToString()
    {
        var handString = "";
        foreach (var card in PlayerHand.Cards)
        {
            handString += card.CardName + ", ";
        }

        return handString;
    }
}
