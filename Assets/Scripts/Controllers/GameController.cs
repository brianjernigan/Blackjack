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
    
    private int _numCardsInHumanHand;
    private int _numCardsInDealerHand;
    
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
        var cannotHit = _numCardsInHumanHand >= MaxNumberOfCardsInHand || _humanPlayer.PlayerHand.HasBusted ||
                        _humanPlayer.PlayerHand.HasBlackjack || _humanPlayer.PlayerHand.HasTwentyOne;
        if (cannotHit) return;
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
        if (_numCardsInHumanHand >= MaxNumberOfCardsInHand) return;
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
    }

    private void SpawnInitialHands()
    {
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
        SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_numCardsInDealerHand], ref _numCardsInDealerHand, _cpuDealer);
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
        SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_numCardsInDealerHand], ref _numCardsInDealerHand, _cpuDealer);
    }

    private void SpawnCard(Card cardOnScreen, ref int cardCount, IPlayer activePlayer)
    {
        // Where to spawn
        var spawnZone = activePlayer switch
        {
            Human => _humanCardZones,
            Dealer => _dealerCardZones,
            _ => null
        };

        if (spawnZone == null) return;
        // What to spawn
        var cardToSpawn = Instantiate(_cardPrefab, spawnZone[cardCount]);
        cardToSpawn.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
        cardCount++;
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
