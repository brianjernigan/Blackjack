using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected const int TwentyOne = 21;
    public bool IsActive { get; set; }
    protected Hand PlayerHand { get; set; } = new();

    public event Action<Player, Card> OnHit;
    protected void RaiseOnHit(Card card)
    {
        OnHit?.Invoke(this, card);
    }

    public event Action OnBusted;
    protected void CheckForBust()
    {
        if (HasBusted)
        {
            OnBusted?.Invoke();
        }
    }

    public event Action OnStay;
    private void RaiseOnStay()
    {
        OnStay?.Invoke();
    }
    
    protected void CheckForBlackjackOr21()
    {
        if (HasBlackjack || HasTwentyOne)
        {
            RaiseOnStay();
        }
    }
    
    public bool HasBusted => PlayerHand.CalculateHandScore() > TwentyOne;
    public virtual bool HasBlackjack => PlayerHand.CalculateHandScore() == TwentyOne && PlayerHand.HasTwoCards();
    public virtual bool HasTwentyOne => PlayerHand.CalculateHandScore() == TwentyOne && !PlayerHand.HasTwoCards();
    public virtual int Score => PlayerHand.CalculateHandScore();
    public int NumCardsInHand => PlayerHand.CardsInHand.Count;

    public abstract void Hit();

    public void Stay()
    {
        IsActive = false;
        RaiseOnStay();
    }
}

