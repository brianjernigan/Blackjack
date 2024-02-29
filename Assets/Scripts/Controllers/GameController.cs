using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _dealButton;
    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _stayButton;

    [Header("Deck")]
    private Deck _gameDeck;
    [SerializeField] private List<Card> _cards;
    
    [Header("Players")]
    private HumanPlayer _humanPlayer;
    private ComputerPlayer _cpuPlayer;
    
    private void InitializePlayers()
    {
        _humanPlayer = new HumanPlayer("Brian");
        _cpuPlayer = new ComputerPlayer();
    }

    private void InitializeDeck()
    {
        _gameDeck = new Deck(_cards);
        Debug.Log(_gameDeck.Cards[0]);
    }
    
    private void Start()
    {
        InitializePlayers();
        InitializeDeck();
    }

    public void OnClickDealButton()
    {
        // Deal two cards to each player
        // Assign the two cards to players' hands
        // Set card images
        // Reveal only one of the computer's cards
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
