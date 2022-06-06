using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButtons : MonoBehaviour
{
    public string mainMenu = "MainMenu";
    public string firstLevel = "NewMainLevel 1";

    public void playAgain()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
