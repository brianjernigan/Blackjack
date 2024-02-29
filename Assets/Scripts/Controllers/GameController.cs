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
    private HumanPlayer _humanPlayer;
    private ComputerPlayer _cpuPlayer;
    
    private void InitializePlayers()
    {
        _humanPlayer = new HumanPlayer();
        _cpuPlayer = new ComputerPlayer();
    }

    private void InitializePlayerHands()
    {
        _humanPlayer.Hand = _gameDeck.InitialDeal();
        _cpuPlayer.Hand = _gameDeck.InitialDeal();
    }
    
    private void Start()
    {
        _gameDeck = new Deck();
        InitializePlayers();
        InitializePlayerHands();
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
