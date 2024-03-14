using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    private GameObject _dealerHiddenCard;

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
        ActivatePlayerButtons();
        _humanPlayer.IsActive = true;
    }

    public void OnClickHitButton()
    {
        var cannotHit = _numCardsInHumanHand >= MaxNumberOfCardsInHand || _humanPlayer.PlayerHand.HasBusted ||
                        _humanPlayer.PlayerHand.HasBlackjack || _humanPlayer.PlayerHand.HasTwentyOne;
        if (cannotHit) return;
        _humanPlayer.Hit(_cpuDealer.DealCard);
        SpawnAdditionalHumanCards();
    }

    public void OnClickStayButton()
    {
        DeactivatePlayerButtons();
        // End turn
        _humanPlayer.Stay();
        // Start cpu turn
        _cpuDealer.IsActive = true;
        // Flip Dealer Card
        _cpuDealer.FlipHiddenCard();
        // Flip Dealer Card On Screen
        _dealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
        // Hit or stay
    }

    private void SpawnAdditionalHumanCards()
    {
        if (_numCardsInHumanHand >= MaxNumberOfCardsInHand) return;
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
    }

    private void SpawnInitialHands()
    {
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
        // Assign first spawned dealer card to variable for revealing
        _dealerHiddenCard = SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_numCardsInDealerHand], ref _numCardsInDealerHand, _cpuDealer);
        SpawnCard(_humanPlayer.PlayerHand.CardsInHand[_numCardsInHumanHand], ref _numCardsInHumanHand, _humanPlayer);
        SpawnCard(_cpuDealer.PlayerHand.CardsInHand[_numCardsInDealerHand], ref _numCardsInDealerHand, _cpuDealer);
    }

    private GameObject SpawnCard(Card cardOnScreen, ref int cardCount, IPlayer activePlayer)
    {
        // Where to spawn
        var spawnZone = activePlayer switch
        {
            Human => _humanCardZones,
            Dealer => _dealerCardZones,
            _ => null
        };

        if (spawnZone == null) return null;
        // What to spawn
        var cardToSpawn = Instantiate(_cardPrefab, spawnZone[cardCount]);
        cardToSpawn.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
        cardCount++;
        return cardToSpawn;
    }

    private void ActivatePlayerButtons()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
        _dealButton.gameObject.SetActive(false);
        _dividerBar.SetActive(true);
    }

    private void DeactivatePlayerButtons()
    {
        _hitButton.gameObject.SetActive(false);
        _stayButton.gameObject.SetActive(false);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
