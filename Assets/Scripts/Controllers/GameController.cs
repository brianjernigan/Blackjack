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
    private readonly List<Player> _playerList = new();

    [Header("On-Screen Elements - Dealing")]
    [SerializeField] private Transform[] _humanCardZones;
    [SerializeField] private Transform[] _dealerCardZones;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;
    public GameObject _dealerHiddenCard;

    private const int MaxNumberOfCardsInHand = 11;
    
    private void InitializePlayers()
    {
        _cpuDealer = new Dealer(_gameDeck, this);
        _humanPlayer = new Human(_cpuDealer, this);
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
        ActivatePlayerActionButtons();
        _humanPlayer.IsActive = true;
    }

    public void OnClickHitButton()
    {
        var cannotHit = _humanPlayer.NumCardsInHand >= MaxNumberOfCardsInHand || _humanPlayer.HasBusted ||
                        _humanPlayer.HasBlackjack || _humanPlayer.HasTwentyOne;
        if (cannotHit) return;
        _humanPlayer.Hit();
    }

    public void OnClickStayButton()
    {
        SwitchTurns();
        RevealDealerCard();
        // CPU evaluation and actions
        StartCoroutine(CpuTurn());
    }

    private void SwitchTurns()
    {
        DeactivatePlayerActionButtons();
        _humanPlayer.Stay();
        _cpuDealer.IsActive = true;
    }

    private void RevealDealerCard()
    {
        _cpuDealer.FlipHiddenCard();
        _dealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
    }

    private IEnumerator CpuTurn()
    {
        while (_cpuDealer.PlayerHandScore < 17)
        {
            yield return new WaitForSeconds(2.0f);
            _cpuDealer.Hit();
        }
        _cpuDealer.Stay();
    }

    public GameObject SpawnCard(Card cardOnScreen, int cardCount, Player activePlayer)
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
        return cardToSpawn;
    }

    private void ActivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
        _dealButton.gameObject.SetActive(false);
        _dividerBar.SetActive(true);
    }

    private void DeactivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(false);
        _stayButton.gameObject.SetActive(false);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
