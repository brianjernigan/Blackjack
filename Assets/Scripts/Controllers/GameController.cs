using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _dealButton;
    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _stayButton;
    
    private Deck _deck;

    private void Start()
    {
        _deck = new Deck();
        Debug.Log(_deck.GameDeck[0]);
        Debug.Log(_deck.GameDeck[0].CardValue);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
