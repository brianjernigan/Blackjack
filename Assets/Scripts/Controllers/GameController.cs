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

    [Header("Deck")]
    [SerializeField] private Deck _gameDeck;
    
    [Header("Players")]
    private HumanPlayer _humanPlayer;
    private ComputerPlayer _cpuPlayer;
    
    private void InitializePlayers()
    {
        _humanPlayer = new HumanPlayer("Brian");
        _cpuPlayer = new ComputerPlayer();
    }
    
    private void Start()
    {
        InitializePlayers();
    }

    public void OnClickDealButton()
    {
        // Draw cards
        // Assign 2 cards to player and cpu
        // Instantiate cards on screen
        // Set card images
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
