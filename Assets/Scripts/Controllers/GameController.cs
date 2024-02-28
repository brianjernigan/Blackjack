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
    
    private Deck _gameDeck;

    private void Start()
    {
        _gameDeck = new Deck();
        Debug.Log(_gameDeck.NewDeck[0]);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
