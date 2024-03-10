using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : IPlayer
{
   public bool IsActive { get; set; }
   public Hand PlayerHand { get; set; } = new();

   public void Hit(Dealer dealer)
   {
      dealer.DealCard(this);
   }

   public void Stay()
   {
      throw new NotImplementedException();
   }
   public override string ToString()
   {
      return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
   }
}
