using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
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
    [SerializeField] private List<Card> _allCards;
    
    [Header("Players")]
    private Human _humanPlayer;
    private Dealer _cpuDealer;
    private readonly List<IPlayer> _playerList = new();

    [Header("On-Screen Elements - Dealing")]
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Transform _humanHandZone;
    [SerializeField] private Transform _cpuPlayerHandZone;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;
    
    private void InitializePlayers()
    {
        _humanPlayer = new Human();
        _cpuDealer = new Dealer(_gameDeck);
        _playerList.Add(_humanPlayer);
        _playerList.Add(_cpuDealer);
    }
    
    private void Start()
    {
        _gameDeck = new Deck(_allCards);
        InitializePlayers();
    }

    public void OnClickDealButton()
    {
        // Deal initial hands
        _cpuDealer.DealInitialHands(_playerList);
        // Set card images
        // Reveal only one of the computer's cards
        ActivateInGameElements();
        Debug.Log(_humanPlayer);
        Debug.Log(_cpuDealer);
    }

    public void OnClickHitButton()
    {
        // Deal new card and add to hand
        _humanPlayer.Hit(_cpuDealer);
        Debug.Log(_humanPlayer);
    }

    public void OnClickStayButton()
    {
        // End turn
        // Start cpu turn
    }

    private void ActivateInGameElements()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
        _dealButton.gameObject.SetActive(false);
        _dividerBar.SetActive(true);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
