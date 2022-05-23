using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider par)
    {
        //disable on collision
        this.gameObject.SetActive(false);

        //update battery manager
        this.transform.parent.GetComponent<BatteryManager>().updateBattery();
    }
}
