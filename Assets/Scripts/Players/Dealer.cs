using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Dealer : IPlayer
{
    public Hand Hand { get; set; }
    public Deck Deck { get; set; }

    public Dealer(Deck gameDeck)
    {
        Deck = gameDeck;
        Hand = new Hand();
    }
    
    public void DealInitialHands(IPlayer human, IPlayer dealer)
    {
        human.Hand.AddCard(Deck.DrawCard());
        human.Hand.AddCard(Deck.DrawCard());
        dealer.Hand.AddCard(Deck.DrawCard());
        dealer.Hand.AddCard(Deck.DrawCard());
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
