using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : IPlayer
{
    public bool IsActive { get; set; }
    public Hand PlayerHand { get; set; } = new();
    
    private Deck GameDeck { get; }
    public Card HiddenCard { get; set; }
    
    // Constructor
    public Dealer(Deck gameDeck)
    {
        GameDeck = gameDeck;
        IsActive = false;
    }
    
    public void Hit(HitDelegate hit)
    {
        hit(this);
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }

    private Card DrawCardFromDeck()
    {
        if (!GameDeck.CardsInDeck.Any())
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        var topCard = GameDeck.CardsInDeck[0];
        GameDeck.CardsInDeck.RemoveAt(0);
        return topCard;
    }

    public void DealCard(IPlayer activePlayer)
    {
        var topCard = DrawCardFromDeck();
        activePlayer.PlayerHand.AddCardToHand(topCard);
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
        HiddenCard = PlayerHand.CardsInHand[0];
        FlipHiddenCard();
    }

    public void FlipHiddenCard()
    {
        HiddenCard.IsHidden = !HiddenCard.IsHidden;
    }
}
