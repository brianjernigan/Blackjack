using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] private int _cardValue;
    [SerializeField] private string _cardName;
    [SerializeField] private Sprite _frontOfCardSprite;
    [SerializeField] private Sprite _backOfCardSprite;

    // Resets scriptable object state when loaded
    private void OnEnable()
    {
        IsHidden = false;
    }

    public int CardValue
    {
        get => _cardValue;
        set => _cardValue = value;
    }

    public string CardName => _cardName;

    public bool IsHidden { get; set; }
    
    public Sprite CardSprite => IsHidden ? _backOfCardSprite : _frontOfCardSprite;
}
