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
    [SerializeField] private Transform[] _humanCardZones;
    [SerializeField] private Transform[] _dealerCardZones;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;

    private const int MaxNumberOfCardsInHand = 11;
    
    private int _activeHumanCardNumber;
    private int _activeDealerCardNumber;
    
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
        _cpuDealer.DealInitialHands(_playerList);
        SpawnInitialHands();
        ActivateInGameElements();
    }

    public void OnClickHitButton()
    {
        if (_activeHumanCardNumber >= MaxNumberOfCardsInHand || _humanPlayer.PlayerHand.HasBusted) return;
        _humanPlayer.Hit(_cpuDealer);
        SpawnAdditionalHumanCards();
    }

    public void OnClickStayButton()
    {
        // End turn
        // Start cpu turn
    }

    private void SpawnAdditionalHumanCards()
    {
        if (_activeHumanCardNumber >= MaxNumberOfCardsInHand) return;
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_activeHumanCardNumber], _activeHumanCardNumber, _humanPlayer);
        _activeHumanCardNumber++;
    }

    private void SpawnInitialHands()
    {
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_activeHumanCardNumber], _activeHumanCardNumber, _humanPlayer);
        SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_activeDealerCardNumber], _activeDealerCardNumber, _cpuDealer);
        _activeHumanCardNumber++;
        _activeDealerCardNumber++;
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_activeHumanCardNumber], _activeHumanCardNumber, _humanPlayer);
        SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_activeDealerCardNumber], _activeDealerCardNumber, _cpuDealer);
        _activeHumanCardNumber++;
        _activeDealerCardNumber++;
    }

    private void SpawnCard(Card cardOnScreen, int cardNumber, IPlayer activePlayer)
    {
        var spawnZone = activePlayer switch
        {
            Human => _humanCardZones,
            Dealer => _dealerCardZones,
            _ => null
        };

        if (spawnZone == null) return;
        var cardToSpawn = Instantiate(_cardPrefab, spawnZone[cardNumber]);
        cardToSpawn.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
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
