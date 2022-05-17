using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    //to add more battery spawn locations, add more battery prefabs under the battery spawner game object

    public int batteryCount;

    void Start()
    {
        //disable all
        for (int x = 0; x < this.transform.childCount; x++)
        {
            this.transform.GetChild(x).gameObject.SetActive(false);
        }

        //pick 5 batteries to enable
        int count = 0;

        while (count < batteryCount)
        {
            int random = Random.Range(0, this.transform.childCount);

            if (this.transform.GetChild(random).gameObject.activeInHierarchy == false)
            {
                this.transform.GetChild(random).gameObject.SetActive(true);
                count++;
            }
        }
    }

    public void SpawnNew()
    {
        //spawn new
        int newBattery = Random.Range(0, this.transform.childCount);

        if (this.transform.GetChild(newBattery).gameObject.activeInHierarchy == false)
        {
            this.transform.GetChild(newBattery).gameObject.SetActive(true);
        }
    }
}
