//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using System;

public abstract class Player
{
    protected const int TwentyOne = 21;

    private bool _isActive;

    protected Hand PlayerHand { get; set; } = new();
    public bool HasBusted => PlayerHand.CalculateHandScore() > TwentyOne;
    public virtual int Score => PlayerHand.CalculateHandScore();
    public int NumCardsInHand => PlayerHand.CardsInHand.Count;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            if (_isActive) CheckForBlackjackOr21();
        }
    }


    // Natural 21
    public virtual bool HasBlackjack => PlayerHand.CalculateHandScore() == TwentyOne && PlayerHand.HasTwoCards();

    // "Unnatural 21"
    public virtual bool HasTwentyOne => PlayerHand.CalculateHandScore() == TwentyOne && !PlayerHand.HasTwoCards();

    // Hitting
    public abstract void Hit();
    public event Action<Player, Card> OnHit;

    protected void RaiseOnHit(Card card)
    {
        OnHit?.Invoke(this, card);
    }

    // Busting
    public event Action OnBusted;

    protected void CheckForBust()
    {
        if (HasBusted) OnBusted?.Invoke();
    }

    // Standing
    public void Stay()
    {
        IsActive = false;
        RaiseOnStay();
    }

    public event Action OnStay;

    private void RaiseOnStay()
    {
        OnStay?.Invoke();
    }

    protected void CheckForBlackjackOr21()
    {
        // Automatically Stay on 21
        if (HasBlackjack || HasTwentyOne) RaiseOnStay();
    }
}