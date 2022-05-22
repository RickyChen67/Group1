using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    // Used to randomly select an exit and placing the enemy at that exit

    public Transform enemy;

    //to add more exits, add more shelf prefabs under the exit spawner prefab
    void Start()
    {
        // Gets a random integer to randomly select an exit shelf to be set inactive
        int randomShelf = Random.Range(0, this.transform.childCount);
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        enemy.position = this.transform.GetChild(randomShelf).transform.position;
        this.transform.GetChild(randomShelf).gameObject.SetActive(false);
    }
}
