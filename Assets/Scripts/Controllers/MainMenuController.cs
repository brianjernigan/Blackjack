//////////////////////////////////////////////
//Assignment/Lab/Project: Blackjack
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 03/18/2024
/////////////////////////////////////////////

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