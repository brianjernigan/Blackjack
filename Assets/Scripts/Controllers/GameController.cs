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
    private Player _humanPlayer;
    private Player _cpuPlayer;

    [Header("On-Screen Elements - Dealing")]
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Transform _humanHandZone;
    [SerializeField] private Transform _cpuPlayerHandZone;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;
    
    private const float HorizontalPadding = 100f;
    
    private void InitializePlayers()
    {
        _humanPlayer = new Player();
        _cpuPlayer = new Player();
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
        ActivateInGameElements();
        SpawnHand(_humanPlayer.Hand, _humanHandZone);
    }

    public void OnClickHitButton()
    {
        // Deal new card and add to hand
        _humanPlayer.Hand.AddCard(_gameDeck.DrawCard());
        _humanHandZone.transform.position = new Vector3(_humanHandZone.transform.position.x - HorizontalPadding,
            _humanHandZone.transform.position.y, _humanHandZone.transform.position.z);
        SpawnHand(_humanPlayer.Hand, _humanHandZone);
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

    private void SpawnHand(Hand hand, Transform zone)
    {
        for (int i = 0; i < hand.Cards.Count; i++)
        {
            var startingPos = zone.transform.position;
            var spawnPos = startingPos + i * HorizontalPadding * Vector3.right.normalized;
            var cardOnScreen = Instantiate(_cardPrefab, spawnPos, Quaternion.identity, zone);
            cardOnScreen.GetComponent<Image>().sprite = hand.Cards[i].CardSprite;
        }
    }

    private void SpawnCard(Card card, Transform zone, List<Card> instantiatedCards)
    {
        var spawnPos = zone.transform.position + instantiatedCards.Count * HorizontalPadding * Vector3.right.normalized;
        var cardOnScreen = Instantiate(_cardPrefab, spawnPos, Quaternion.identity, zone);
        cardOnScreen.GetComponent<Image>().sprite = card.CardSprite;
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
