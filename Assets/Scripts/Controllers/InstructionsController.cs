//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

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