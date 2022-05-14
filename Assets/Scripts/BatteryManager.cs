using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    void OnTriggerEnter(Collider par) 
    {
        //disable on collision
        this.gameObject.SetActive(false);

        //spawn new
        this.transform.parent.GetComponent<BatterySpawner>().SpawnNew();

        //refill flashlight
        GameObject flashlight = GameObject.Find("Flashlight");
        flashlight.GetComponent<FlashlightManager>().Refill();
    }
}