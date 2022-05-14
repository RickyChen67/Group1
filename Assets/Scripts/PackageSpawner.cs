using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    //to add more package spawn locations, add more package prefabs under the package spawner game object

    void Start()
    {
        //disable all
        for (int x = 0; x < this.transform.childCount; x++)
        {
            this.transform.GetChild(x).gameObject.SetActive(false);
        }

        //pick a random package to enable
        this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject.SetActive(true);
    }
}
