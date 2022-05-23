using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    //to add more exits, add more shelf prefabs under the exit spawner prefab

    private bool hasPackage = false;

    void Start()
    {
        //pick a random shelf to disable
        this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider par)
    {
        //load ending on collision
        if (hasPackage == true)
        {
            SceneManager.LoadScene("TrueEnding");
        }
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void UpdateExit(bool packageStatus)
    {
        //update package status
        hasPackage = packageStatus;
    }
}
