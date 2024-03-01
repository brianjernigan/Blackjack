using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<Card> _cards;
    
    [Header("Players")]
    private HumanPlayer _humanPlayer;
    private ComputerPlayer _cpuPlayer;

    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _humanHandZone;
    [SerializeField] private Transform _cpuPlayerHandZone;
    
    private void InitializePlayers()
    {
        _humanPlayer = new HumanPlayer("Brian");
        _cpuPlayer = new ComputerPlayer();
    }
    
    private void Start()
    {
        InitializePlayers();
        _gameDeck = new Deck(_cards);
    }

    public void OnClickDealButton()
    {
        _humanPlayer.Hand = _gameDeck.DealInitialHand();
        _cpuPlayer.Hand = _gameDeck.DealInitialHand();
        // Set card images
        // Reveal only one of the computer's cards
        ActivateInGameButtons();
        InstantiateCard(_humanPlayer.Hand.Cards[0], _humanHandZone);
    }

    public void OnClickHitButton()
    {
        // Deal new card and add to hand
        _humanPlayer.Hand.AddCard(_gameDeck.DrawCard());
        PrintHand(_humanPlayer.Hand);
    }

    public void OnClickStayButton()
    {
        // End turn
        // Start cpu turn
    }

    private void ActivateInGameButtons()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
        _dealButton.gameObject.SetActive(false);
    }

    private void InstantiateCard(Card cardOnScreen, Transform zone)
    {
        var cardElement = Instantiate(_cardPrefab, _humanHandZone);
        cardElement.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
    }

    private void PrintHand(Hand hand)
    {
        foreach (var card in hand.Cards)
        {
            Debug.Log(card.CardName);
        }
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
