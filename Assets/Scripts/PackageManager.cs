using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    //the boolean for hasPackage is ready, but needs to be hooked up to trigger the ending

    public GameObject package;
    public bool hasPackage;

    void Start()
    {
        //hide hold package
        package = GameObject.Find("HoldingPackage");
        package.GetComponent<Renderer>().enabled = false;

        hasPackage = false;
    }

    void OnTriggerEnter(Collider par)
    {
        //disable on collision
        this.gameObject.SetActive(false);

        //show holding package
        package.GetComponent<Renderer>().enabled = true;

        hasPackage = true;
    }
}