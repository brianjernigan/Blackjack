//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Action Buttons")] 
    [SerializeField] private Button _dealButton;
    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _stayButton;
    
    [Header("Playing Field")] 
    [SerializeField] private TMP_Text _humanScoreText;
    [SerializeField] private TMP_Text _dealerScoreText;
    [SerializeField] private GameObject _dividerBar;
    
    [Header("On-Screen Elements - Dealing")] 
    [SerializeField] private Transform[] _humanCardZones;
    [SerializeField] private Transform[] _dealerCardZones;
    [SerializeField] private GameObject _cardPrefab;

    [Header("Game Over")] 
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private TMP_Text _gameResultText;
    
    // On-screen hidden card
    public GameObject DealerHiddenCard { get; set; }
    private GameManager _gm;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    public void ActivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(true);
        _stayButton.gameObject.SetActive(true);
    }

    public void DeactivatePlayerActionButtons()
    {
        _hitButton.gameObject.SetActive(false);
        _stayButton.gameObject.SetActive(false);
    }

    public void OnInitialDeal()
    {
        _dealButton.gameObject.SetActive(false);
        _dividerBar.gameObject.SetActive(true);
    }

    public void UpdateScoreText(Player activePlayer)
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


    public void DisplayResults()
    {
        _endGamePanel.SetActive(true);
        _gameResultText.text = _gm.DetermineWinner();
        _dividerBar.SetActive(false);
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
        if (cardCount >= spawnZone.Length) return null;
        var cardToSpawn = Instantiate(_cardPrefab, spawnZone[cardCount]);
        cardToSpawn.GetComponent<Image>().sprite = cardOnScreen.CardSprite;
        return cardToSpawn;
    }
}
