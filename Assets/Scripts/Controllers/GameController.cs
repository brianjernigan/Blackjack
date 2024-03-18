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

public class GameController : MonoBehaviour
{
    // Worst case 
    private const int MaxNumberOfCardsInHand = 11;

    [Header("Buttons")] [SerializeField] private Button _dealButton;

    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _stayButton;
    [SerializeField] private List<Card> _allCards;

    [Header("On-Screen Elements - Dealing")] [SerializeField]
    private Transform[] _humanCardZones;

    [SerializeField] private Transform[] _dealerCardZones;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _dividerBar;

    [Header("Scores")] [SerializeField] private TMP_Text _humanScoreText;

    [SerializeField] private TMP_Text _dealerScoreText;

    [Header("Game Over")] [SerializeField] private GameObject _endGamePanel;

    [SerializeField] private TMP_Text _gameResultText;
    [SerializeField] private Button _playAgainButton;
    private readonly List<Player> _playerList = new();
    private Dealer _cpuDealer;
    private GameState _currentState;

    [Header("Deck")] private Deck _gameDeck;

    [Header("Players")] private Human _humanPlayer;

    // On-screen hidden card
    private GameObject DealerHiddenCard { get; set; }

    private void Start()
    {
        SetGameState(GameState.Initializing);
    }

    private void OnDestroy()
    {
        UnsubscribeToHumanEvents();
        UnsubscribeToDealerEvents();
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


    private void InitialDeal()
    {
        _cpuDealer.DealInitialHands(_playerList);
        _dealButton.gameObject.SetActive(false);
        _dividerBar.SetActive(true);
        SetGameState(GameState.HumanTurn);
    }


    private void StartHumanTurn()
    {
        _humanPlayer.IsActive = true;
        if (_humanPlayer.HasBlackjack) return;
        ActivatePlayerActionButtons();
    }

    private void EndGame()
    {
        DetermineWinner();
        DisplayResults();
    }

    private string DetermineWinner()
    {
        var resultText = "";
        if (_humanPlayer.HasBusted)
            resultText = "You lose";
        else if (_cpuDealer.HasBusted && !_humanPlayer.HasBusted)
            resultText = "You win";
        else if (_humanPlayer.HasBlackjack && !_cpuDealer.HasBlackjack)
            resultText = "You win";
        else if (_humanPlayer.HasBlackjack && _cpuDealer.HasBlackjack)
            resultText = "Tie!";
        else if (_cpuDealer.HasBlackjack && !_humanPlayer.HasBlackjack)
            resultText = "You lose";
        else if (_humanPlayer.HasTwentyOne && !_cpuDealer.HasTwentyOne)
            resultText = "You win";
        else if (_humanPlayer.HasTwentyOne && _cpuDealer.HasTwentyOne)
            resultText = "Tie";
        else if (!_humanPlayer.HasTwentyOne && _cpuDealer.HasTwentyOne)
            resultText = "You lose";
        else if (_humanPlayer.Score > _cpuDealer.Score)
            resultText = "You win";
        else if (_humanPlayer.Score == _cpuDealer.Score)
            resultText = "Tie";
        else if (_cpuDealer.Score > _humanPlayer.Score) resultText = "You lose";

        return resultText;
    }

    private void HandleHit(Player player, Card card)
    {
        if (player is Dealer && player.NumCardsInHand == 1)
            DealerHiddenCard = SpawnCard(card, player.NumCardsInHand - 1, player);
        else
            SpawnCard(card, player.NumCardsInHand - 1, player);
        UpdateScoreText(player);
    }

    private void HandleHumanBust()
    {
        SetGameState(GameState.RoundOver);
        DeactivatePlayerActionButtons();
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
        DealerHiddenCard.GetComponent<Image>().sprite = _cpuDealer.HiddenCard.CardSprite;
        UpdateScoreText(_cpuDealer);
    }

    private IEnumerator DealerDecision()
    {
        while (_cpuDealer.Score < 17)
        {
            yield return new WaitForSeconds(1.0f);
            _cpuDealer.Hit();
        }

        yield return new WaitForSeconds(1.0f);
        _cpuDealer.Stay();
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
        if (cardCount >= spawnZone.Length) return null;
        var cardToSpawn = Instantiate(_cardPrefab, spawnZone[cardCount]);
        cardToSpawn.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
        return cardToSpawn;
    }

    private void ActivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
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
            playerScoreText.text = "Blackjack!";
        else if (activePlayer.HasBusted)
            playerScoreText.text = "Busted!";
        else
            playerScoreText.text = activePlayer.Score.ToString();
    }


    private void DisplayResults()
    {
        _endGamePanel.SetActive(true);
        _gameResultText.text = DetermineWinner();
        _dividerBar.SetActive(false);
    }

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

    private void SubscribeToHumanEvents()
    {
        _humanPlayer.OnHit += HandleHit;
        _humanPlayer.OnStay += HandleHumanStay;
        _humanPlayer.OnStay += DeactivatePlayerActionButtons;
        _humanPlayer.OnBusted += HandleHumanBust;
        _humanPlayer.OnBusted += DeactivatePlayerActionButtons;
    }

    private void SubscribeToDealerEvents()
    {
        _cpuDealer.OnHit += HandleHit;
        _cpuDealer.OnStay += HandleCpuStay;
    }

    private void UnsubscribeToHumanEvents()
    {
        _humanPlayer.OnHit -= HandleHit;
        _humanPlayer.OnStay -= HandleHumanStay;
        _humanPlayer.OnStay -= DeactivatePlayerActionButtons;
        _humanPlayer.OnBusted -= HandleHumanBust;
        _humanPlayer.OnBusted -= DeactivatePlayerActionButtons;
    }

    private void UnsubscribeToDealerEvents()
    {
        _cpuDealer.OnHit -= HandleHit;
        _cpuDealer.OnStay -= HandleCpuStay;
    }

    #endregion
}