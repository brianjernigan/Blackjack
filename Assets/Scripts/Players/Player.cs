using System;
using System.Collections;
using UnityEngine;

public abstract class Player
{
    public void Hit(Dealer dealer)
    {
        dealer.DealCard(this);
    }

    public void Stay()
    {
        throw new NotImplementedException();
    }
}
