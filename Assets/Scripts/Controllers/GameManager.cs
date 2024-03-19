//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Worst case 
    private const int MaxNumberOfCardsInHand = 11;
    private const string WinString = "You win!";
    private const string LoseString = "You lose!";
    private const string TieString = "Tie.";
    
    [Header("Deck")] 
    private Deck _gameDeck;
    [SerializeField] private List<Card> _allCards;

    [Header("Players")] 
    private Human _humanPlayer;
    private Dealer _cpuDealer;
    private readonly List<Player> _playerList = new();
    
    private GameState _currentState;
    private UIManager _ui;

    private void Awake()
    {
        _ui = FindObjectOfType<UIManager>();
    }
    
    private void Start()
    {
        SetGameState(GameState.Initializing);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
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
                InitialDeal();
                break;
            case GameState.HumanTurn:
                StartHumanTurn();
                break;
            case GameState.DealerTurn:
                StartDealerTurn();
                break;
            case GameState.RoundOver:
                EndGame();
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
        SubscribeEvents();
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


    private void InitialDeal()
    {
        _cpuDealer.DealInitialHands(_playerList);
        _ui.OnInitialDeal();
        SetGameState(GameState.HumanTurn);
    }


    private void StartHumanTurn()
    {
        _humanPlayer.IsActive = true;
        if (_humanPlayer.HasBlackjack) return;
        _ui.ActivatePlayerActionButtons();
    }

    private void EndGame()
    {
        DetermineWinner();
        _ui.DisplayResults();
    }

    private void HandleHit(Player player, Card card)
    {
        // Assign On-screen hidden card on first hit
        if (player is Dealer && player.NumCardsInHand == 1)
            _ui.DealerHiddenCard = _ui.SpawnCard(card, player.NumCardsInHand - 1, player);
        else
            _ui.SpawnCard(card, player.NumCardsInHand - 1, player);
        _ui.UpdateScoreText(player);
    }

    private void HandleHumanBust()
    {
        SetGameState(GameState.RoundOver);
        _ui.DeactivatePlayerActionButtons();
    }

    private void HandleHumanStay()
    {
        SetGameState(GameState.DealerTurn);
    }

    private void HandleCpuStay()
    {
        StopCoroutine(DealerDecision());
        SetGameState(GameState.RoundOver);
    }

    private void StartDealerTurn()
    {
        _cpuDealer.IsActive = true;
        RevealDealerCard();
        if (_humanPlayer.HasBlackjack && !_cpuDealer.HasBlackjack)
        {
            SetGameState(GameState.RoundOver);
            return;
        }

        StartCoroutine(DealerDecision());
    }

    private void RevealDealerCard()
    {
        _cpuDealer.FlipHiddenCard();
        _ui.DealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
        _ui.UpdateScoreText(_cpuDealer);
    }

    private IEnumerator DealerDecision()
    {
        while (_cpuDealer.Score < 17)
        {
            yield return new WaitForSeconds(1.0f);
            _cpuDealer.Hit();
        }
        
        _cpuDealer.Stay();
    }

    public string DetermineWinner()
    {
        if (_humanPlayer.HasBusted) return LoseString;
        if (_cpuDealer.HasBusted) return WinString;

        if (BothPlayersHaveBlackjack()) return TieString;
        if (OnePlayerHasBlackjack(_humanPlayer, _cpuDealer)) return WinString;
        if (OnePlayerHasBlackjack(_cpuDealer, _humanPlayer)) return LoseString;

        if (BothPlayersHaveTwentyOne()) return TieString;
        if (OnePlayerHasTwentyOne(_humanPlayer, _cpuDealer)) return WinString;
        if (OnePlayerHasTwentyOne(_cpuDealer, _humanPlayer)) return LoseString;

        return CompareScores();
    }
    
    #region GameOutcomes
    private bool BothPlayersHaveBlackjack()
    {
        return _humanPlayer.HasBlackjack && _cpuDealer.HasBlackjack;
    }

    private bool OnePlayerHasBlackjack(Player player, Player opponent)
    {
        return player.HasBlackjack && !opponent.HasBlackjack;
    }

    private bool BothPlayersHaveTwentyOne()
    {
        return _humanPlayer.HasTwentyOne && _cpuDealer.HasTwentyOne;
    }

    private bool OnePlayerHasTwentyOne(Player player, Player opponent)
    {
        return player.HasTwentyOne && !opponent.HasTwentyOne;
    }

    private string CompareScores()
    {
        if (_humanPlayer.Score > _cpuDealer.Score) return WinString;
        return _humanPlayer.Score < _cpuDealer.Score ? LoseString : TieString;
    }
    #endregion

    #region ButtonClicks

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
        _humanPlayer.Stay();
    }

    public void OnClickPlayAgainButton()
    {
        SceneManager.LoadScene("Blackjack");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    #endregion

    #region EventSubscriptions

    private void SubscribeEvents()
    {
        _humanPlayer.OnHit += HandleHit;
        _humanPlayer.OnStay += HandleHumanStay;
        _humanPlayer.OnStay += _ui.DeactivatePlayerActionButtons;
        _humanPlayer.OnBusted += HandleHumanBust;
        _humanPlayer.OnBusted += _ui.DeactivatePlayerActionButtons;
        _cpuDealer.OnHit += HandleHit;
        _cpuDealer.OnStay += HandleCpuStay;
    }

    private void UnsubscribeEvents()
    {
        _humanPlayer.OnHit -= HandleHit;
        _humanPlayer.OnStay -= HandleHumanStay;
        _humanPlayer.OnStay -= _ui.DeactivatePlayerActionButtons;
        _humanPlayer.OnBusted -= HandleHumanBust;
        _humanPlayer.OnBusted -= _ui.DeactivatePlayerActionButtons;
        _cpuDealer.OnHit -= HandleHit;
        _cpuDealer.OnStay -= HandleCpuStay;
    }

    #endregion
}