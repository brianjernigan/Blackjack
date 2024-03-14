using System;
using System.Collections;
using UnityEngine;

public abstract class Player
{
    public bool IsActive { get; set; }
    public Hand PlayerHand { get; set; } = new();

    public void Hit(Dealer dealer)
    {
        var cardToAdd = dealer.DealCard();
        PlayerHand.CardsInHand.Add(cardToAdd);
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }
}
