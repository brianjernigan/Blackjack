using System;
using System.Collections;
using UnityEngine;

public class Human : IPlayer
{
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
      var handString = "";
      foreach (var card in PlayerHand.Cards)
      {
         handString += card.CardName + ", ";
      }

      return handString;
   }
}
