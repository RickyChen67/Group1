using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider par)
    {
        //disable on collision
        this.gameObject.SetActive(false);

        //update package manager
        this.transform.parent.transform.parent.gameObject.GetComponent<PackageManager>().UpdatePackage(true);
    }
}