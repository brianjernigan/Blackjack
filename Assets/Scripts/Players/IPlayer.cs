using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public delegate void HitDelegate(IPlayer activePlayer);

public interface IPlayer
{
    bool IsActive { get; set; }
    Hand PlayerHand { get; set; }
    
    // When called from GameController, pass Dealer's DealCard method as delegate parameter
    // Probably unnecessary but fun to experiment
    void Hit(HitDelegate hit);
    void Stay();

    string ToString()
    {
        return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
    }
}
