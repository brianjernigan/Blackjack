using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Blackjack");
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
