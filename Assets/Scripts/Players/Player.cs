using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    private const int TwentyOne = 21;
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
    
    public event Action OnBlackjack;
    protected void CheckForBlackjack()
    {
        if (HasBlackjack)
        {
            OnBlackjack?.Invoke();
        }
    }
    
    public bool HasBusted => PlayerHand.CalculateHandScore() > TwentyOne;
    public bool HasBlackjack => PlayerHand.CalculateHandScore() == TwentyOne && PlayerHand.HasTwoCards();
    public bool HasTwentyOne => PlayerHand.CalculateHandScore() == TwentyOne;

    public virtual int Score => PlayerHand.CalculateHandScore();

    public int NumCardsInHand => PlayerHand.CardsInHand.Count;

    public abstract void Hit();

    public void Stay()
    {
        IsActive = false;
        RaiseOnStay();
    }
}

