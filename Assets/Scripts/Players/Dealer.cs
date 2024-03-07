using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : Player
{
    public Deck Deck { get; set; }
    public DealerHand DealerHand { get; set; }

    public Dealer(Deck gameDeck)
    {
        Deck = gameDeck;
        DealerHand = new DealerHand();
    }
    
    public void DealCard(Player activePlayer)
    {
        Deck.DrawCard(activePlayer);
    }
    
    public void DealInitialHands(List<Player> activePlayers)
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (var player in activePlayers)
            {
                DealCard(player);
            }
        }

        DealerHand.HiddenCard = DealerHand.Cards[0];
        FlipCard(DealerHand.HiddenCard);
    }

    private void FlipCard(Card cardToFlip)
    {
        cardToFlip.IsHidden = !cardToFlip.IsHidden;
    }
    
    public override string ToString()
    {
        var handString = "";
        foreach (var card in DealerHand.Cards)
        {
            handString += card.CardName + ", ";
        }

        return handString;
    }
}
