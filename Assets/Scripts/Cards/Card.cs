//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField] private int _cardValue;
    [SerializeField] private string _cardName;
    [SerializeField] private Sprite _frontOfCardSprite;
    [SerializeField] private Sprite _backOfCardSprite;

    public int CardValue => _cardValue;
    public string CardName => _cardName;

    public bool IsHidden { get; set; }

    // If card is hidden, will return back of card
    public Sprite CardSprite => IsHidden ? _backOfCardSprite : _frontOfCardSprite;
}