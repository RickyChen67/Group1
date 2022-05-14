using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    //to add more exits, add more shelf prefabs under the exit spawner prefab

    void Start()
    {
        //pick a random shelf to disable
        this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject.SetActive(false);
    }
}
