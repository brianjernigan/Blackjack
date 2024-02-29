using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _dealButton;
    [SerializeField] private Button _hitButton;
    [SerializeField] private Button _stayButton;
    
    private HumanPlayer _humanPlayer;
    private ComputerPlayer _cpuPlayer;
    
    private void InitializePlayers()
    {
        _humanPlayer = new HumanPlayer();
        _cpuPlayer = new ComputerPlayer();
    }
    
    private void Start()
    {
        
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
