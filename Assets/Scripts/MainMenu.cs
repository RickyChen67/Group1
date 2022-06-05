using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public GameObject scenarioPage;

    public GameObject optionsModal;

    public GameObject creditsModal;

    public GameObject quitModal;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(waiting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        scenarioPage.SetActive(true);
        // yield return new WaitForSeconds (5.0);
        SceneManager.LoadScene(firstLevel);
    }   

    public void OpenOptions()
    {
        optionsModal.SetActive(true);
    }

    public void closeOptions()
    {
        optionsModal.SetActive(false);
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
