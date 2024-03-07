using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : IPlayer
{
   public Hand Hand { get; set; } = new();
   public Deck Deck { get; set; }

   public Human(Deck gameDeck)
   {
      Deck = gameDeck;
   }

   public void Hit()
   {
      Hand.AddCard(Deck.DrawCard());
   }

   public void Stay()
   {
      throw new NotImplementedException();
   }
}
