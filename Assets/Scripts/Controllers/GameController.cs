using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
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
    private Human _humanPlayer;
    private Dealer _cpuDealer;

    [Header("On-Screen Elements - Dealing")]
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Transform _humanHandZone;
    [SerializeField] private Transform _cpuPlayerHandZone;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;
    
    private void InitializePlayers()
    {
        _humanPlayer = new Human(_gameDeck);
        _cpuDealer = new Dealer(_gameDeck);
    }
    
    private void Start()
    {
        _gameDeck = new Deck(_cards);
        InitializePlayers();
    }

    public void OnClickDealButton()
    {
        // Deal initial hands
        _cpuDealer.DealInitialHands(_humanPlayer, _cpuDealer);
        // Set card images
        // Reveal only one of the computer's cards
        ActivateInGameElements();
    }

    public void OnClickHitButton()
    {
        // Deal new card and add to hand
        _humanPlayer.Hit();
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
