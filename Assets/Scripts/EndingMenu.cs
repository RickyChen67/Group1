using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenu : MonoBehaviour
{
    public string mainMenu = "MainMenu";
    public string firstLevel = "NewMainLevel 1";

    public List<GameObject> endings = new List<GameObject>(2);

    public void Start()
    {
        endings[0].SetActive(false);
        endings[1].SetActive(false);

        endings[Random.Range(0, 2)].SetActive(true);
    }

    public void playAgain()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
