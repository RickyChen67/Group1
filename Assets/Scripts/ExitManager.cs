using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    //to add more exits, add more shelf prefabs under the exit spawner prefab

    private bool hasPackage = false;
    GameObject enemy;

    void Start()
    {
        // Gets a random integer to randomly select an exit shelf to be set inactive
        int randomShelf = Random.Range(0, this.transform.childCount);
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        // Gets the enemy spawn location and sets it as the enemy's position as well as setting its mesh renderer active
        Vector3 enemySpawnLocation = this.transform.GetChild(randomShelf).transform.position;
        enemy.transform.position = new Vector3(enemySpawnLocation.x, 1, enemySpawnLocation.z);
        enemy.GetComponent<MeshRenderer>().enabled = true;

        this.transform.GetChild(randomShelf).gameObject.SetActive(false);
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
