using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : Player
{ 
   public void Stay()
   {
      throw new NotImplementedException();
   }
   public override string ToString()
   {
      return PlayerHand.CardsInHand.Aggregate("", (current, card) => current + (card.CardName + ", "));
   }
}
