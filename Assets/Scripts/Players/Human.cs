using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : Player
{
    private Dealer GameDealer { get; set; }

    public Human(Dealer dealer, GameController gameController)
    {
        IsActive = true;
        GameDealer = dealer;
        GameController = gameController;
    }

    public override void Hit()
    {
        var topCard = GameDealer.DrawCardFromDeck();
        PlayerHand.AddCardToHand(topCard);
        GameController.SpawnCard(PlayerHand.CardsInHand[NumCardsInHand - 1], NumCardsInHand - 1, this);
    }
}
