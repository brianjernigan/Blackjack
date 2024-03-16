using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Scores")] 
    [SerializeField] private TMP_Text _humanScoreText;
    [SerializeField] private TMP_Text _dealerScoreText;

    private GameObject DealerHiddenCard { get; set; }

    private const int MaxNumberOfCardsInHand = 11;
    
    private void InitializePlayers()
    {
        _cpuDealer = new Dealer(_gameDeck);
        _humanPlayer = new Human(_cpuDealer);
        _playerList.Add(_humanPlayer);
        _playerList.Add(_cpuDealer);
        _humanPlayer.OnHit += HandleHit;
        _cpuDealer.OnHit += HandleHit;
        _humanPlayer.OnStay += HandleHumanStay;
    }
    
    private void Start()
    {
        _gameDeck = new Deck(_allCards);
        InitializePlayers();
    }
    

    private void HandleHit(Player player, Card card)
    {
        if (player is Dealer && player.NumCardsInHand == 1)
        {
            DealerHiddenCard = SpawnCard(card, player.NumCardsInHand - 1, player);
        }
        else
        {
            SpawnCard(card, player.NumCardsInHand - 1, player);
        }
        UpdateScoreText(player);
    }

    private void HandleHumanStay()
    {
        DeactivatePlayerActionButtons();
    }

    private void OnDestroy()
    {
        _humanPlayer.OnHit -= HandleHit;
        _cpuDealer.OnHit -= HandleHit;
    }

    private void Update()
    {
        
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
        CheckForBustOr21();
    }

    private void CheckForBustOr21()
    {
        if (_humanPlayer.HasBusted || _humanPlayer.HasTwentyOne)
        {
            _humanPlayer.Stay();
        }
    }

    public void OnClickStayButton()
    {
        _humanPlayer.Stay();
        _cpuDealer.IsActive = true;
        RevealDealerCard();
        // CPU evaluation and actions
        StartCoroutine(CpuTurn());
        _cpuDealer.Stay();
    }

    private void RevealDealerCard()
    {
        _cpuDealer.FlipHiddenCard();
        DealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
    }

    private IEnumerator CpuTurn()
    {
        while (_cpuDealer.Score < 17)
        {
            yield return new WaitForSeconds(1.5f);
            _cpuDealer.Hit();
        }

        yield return null;
    }

    private GameObject SpawnCard(Card cardOnScreen, int cardCount, Player activePlayer)
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

    public void DeactivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(false);
        _stayButton.gameObject.SetActive(false);
    }

    private void UpdateScoreText(Player activePlayer)
    {
        var playerScoreText = activePlayer switch
        {
            Human => _humanScoreText,
            Dealer => _dealerScoreText,
            _ => null
        };

        if (playerScoreText is null) return;
        
        if (activePlayer.HasBlackjack)
        {
            playerScoreText.text = "Blackjack!";
        } else if (activePlayer.HasBusted)
        {
            playerScoreText.text = "Busted!";
        }
        else
        {
            playerScoreText.text = activePlayer.Score.ToString();
        }
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
