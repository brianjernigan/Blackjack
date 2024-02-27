using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Blackjack");
    }

    public void OnClickInstructionsButton()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
