using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : Player
{
    private Dealer GameDealer { get; set; }

    public Human(Dealer dealer)
    {
        GameDealer = dealer;
    }

    public override void Hit()
    {
        var topCard = GameDealer.DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);
        
        RaiseOnHit(topCard);
        if (!IsActive) return;
        CheckForBust();
        CheckForBlackjackOr21();
    }
}
