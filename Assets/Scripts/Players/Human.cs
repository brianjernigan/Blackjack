using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : Player
{
    private Dealer GameDealer { get; set; }

    public Human(Dealer dealer)
    {
        IsActive = true;
        GameDealer = dealer;
    }

    public override void Hit()
    {
        var topCard = GameDealer.DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);
    }
}
