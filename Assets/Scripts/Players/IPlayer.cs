using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public interface IPlayer
{
    bool IsActive { get; set; }
    Hand PlayerHand { get; set; }
    void Hit(Dealer dealer);
    void Stay();

    string ToString()
    {
        return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
    }
}
