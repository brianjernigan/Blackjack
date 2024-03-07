using System;
using System.Collections;
using UnityEngine;

public class Human : Player
{
   public Hand HumanHand { get; set; } = new HumanHand();

   public override string ToString()
   {
      var handString = "";
      foreach (var card in HumanHand.Cards)
      {
         handString += card.CardName + ", ";
      }

      return handString;
   }
}
