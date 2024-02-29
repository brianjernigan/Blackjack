using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class SO_Card : ScriptableObject
{
    public int _cardValue;
    public string _cardName;
    public Sprite _cardSprite;
}
