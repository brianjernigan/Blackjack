using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : Player
{
    private Dealer GameDealer { get; set; }

    public event Action OnStay;

    public Human(Dealer dealer)
    {
        IsActive = true;
        GameDealer = dealer;
    }

    public override void Hit()
    {
        var topCard = GameDealer.DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);

        RaiseOnHit(topCard);
        CheckForBust();
    }

    public override void Stay()
    {
        IsActive = false;
        OnStay?.Invoke();
    }
}
