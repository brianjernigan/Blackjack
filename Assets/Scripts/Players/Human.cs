using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Human : IPlayer
{
   public bool IsActive { get; set; }
   public Hand PlayerHand { get; set; } = new();
   
   public void Hit(HitDelegate hit)
   {
      hit(this);
   }
   
   public void Stay()
   {
      throw new NotImplementedException();
   }
}
