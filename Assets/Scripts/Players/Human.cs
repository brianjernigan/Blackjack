using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : IPlayer
{
   public bool IsActive { get; set; } = true;
   public Hand PlayerHand { get; set; } = new();

   public void Hit(HitDelegate hit)
   {
      hit(this);
   }
}
