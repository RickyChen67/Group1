using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public GameObject scenarioPage;

    public GameObject controlsModal;

    public GameObject creditsModal;

    public GameObject quitModal;

    // Start is called before the first frame update
    void Start()
    {

     }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        scenarioPage.SetActive(true);
        }

    public void continueGame()
    {
        SceneManager.LoadScene(firstLevel);
    } 

    public void openControls()
    {
        controlsModal.SetActive(true);
    }

    public void closeControls()
    {
        controlsModal.SetActive(false);
    }

    public void openCredits()
    {
        creditsModal.SetActive(true);
    }

    public void closeCredits()
    {
        creditsModal.SetActive(false);
    }

    public void openQuit()
    {
        quitModal.SetActive(true);
    }

    public void closeQuit()
    {
        quitModal.SetActive(false);
    }

    public void confirmQuit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
