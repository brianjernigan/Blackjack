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

    private GameState _currentState;
    
    private void Start()
    {
        SetGameState(GameState.Initializing);
    }
    
    private void SetGameState(GameState newState)
    {
        _currentState = newState;
        HandleGameStateChange();
    }

    private void HandleGameStateChange()
    {
        switch (_currentState)
        {
            case GameState.Initializing:
                InitializeGame();
                break;
            case GameState.Dealing:
                _cpuDealer.DealInitialHands(_playerList);
                SetGameState(GameState.PlayerTurn);
                break;
            case GameState.PlayerTurn:
                StartHumanTurn();
                break;
            case GameState.DealerTurn:
                StartDealerTurn();
                break;
            case GameState.RoundOver:
                DetermineGameOutcome();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InitializeGame()
    {
        _gameDeck = new Deck(_allCards);
        InitializePlayers();
        AddPlayersToPlayerList();
        SubscribeToHumanEvents();
        SubscribeToDealerEvents();
    }

    private void InitializePlayers()
    {
        _cpuDealer = new Dealer(_gameDeck);
        _humanPlayer = new Human(_cpuDealer);
    }

    private void AddPlayersToPlayerList()
    {
        _playerList.Add(_humanPlayer);
        _playerList.Add(_cpuDealer);
    }

    private void SubscribeToHumanEvents()
    {
        _humanPlayer.OnHit += HandleHit;
        _humanPlayer.OnStay += HandleHumanStay;
        _humanPlayer.OnBusted += HandleBust;
        _humanPlayer.OnBlackjack += HandleHumanBlackjack;
    }

    private void SubscribeToDealerEvents()
    {
        _cpuDealer.OnHit += HandleHit;
        _cpuDealer.OnStay += HandleCpuStay;
        _cpuDealer.OnBlackjack += HandleCpuBlackjack;
    }

    public void OnClickDealButton()
    {
        SetGameState(GameState.Dealing);
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
        SetGameState(GameState.DealerTurn);
    }

    private void StartHumanTurn()
    {
        _humanPlayer.IsActive = true;
        ActivatePlayerActionButtons();
    }
    
    private void StartDealerTurn()
    {
        _humanPlayer.Stay();
        StartCpuTurn();
    }
    private void DetermineGameOutcome()
    {
        Debug.Log("win/loss");
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

    private void HandleBust()
    {
        SetGameState(GameState.RoundOver);
    }

    private void HandleHumanStay()
    {
        DeactivatePlayerActionButtons();
    }

    private void HandleCpuStay()
    {
        SetGameState(GameState.RoundOver);
    }

    private void HandleHumanBlackjack()
    {
        SetGameState(GameState.DealerTurn);
    }

    private void HandleCpuBlackjack()
    {
        SetGameState(GameState.RoundOver);
    }
    
    private void StartCpuTurn()
    {
        _cpuDealer.IsActive = true;
        RevealDealerCard();
        StartCoroutine(CpuTurn());
    }

    private void RevealDealerCard()
    {
        _cpuDealer.FlipHiddenCard();
        DealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
        UpdateScoreText(_cpuDealer);
    }

    private IEnumerator CpuTurn()
    {
        while (_cpuDealer.Score < 17)
        {
            yield return new WaitForSeconds(1.0f);
            _cpuDealer.Hit();
        }

        _cpuDealer.Stay();
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

    private void DeactivatePlayerActionButtons()
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
    
    private void OnDestroy()
    {
        UnsubscribeToHumanEvents();
        UnsubscribeToDealerEvents();
    }
    
    private void UnsubscribeToHumanEvents()
    {
        _humanPlayer.OnHit -= HandleHit;
        _humanPlayer.OnStay -= HandleHumanStay;
        _humanPlayer.OnBusted -= HandleBust;
        _humanPlayer.OnBlackjack -= HandleHumanBlackjack;
    }
    
    private void UnsubscribeToDealerEvents()
    {
        _cpuDealer.OnHit -= HandleHit;
        _cpuDealer.OnStay -= HandleCpuStay;
        _cpuDealer.OnBlackjack -= HandleCpuBlackjack;
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
