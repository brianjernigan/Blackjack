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
        // GameController.SpawnCard(PlayerHand.CardsInHand[NumCardsInHand - 1], NumCardsInHand - 1, this);
        // GameController.UpdateScoreText(this);
    }

    public override void Stay()
    {
        IsActive = false;
        OnStay?.Invoke();
    }
}
