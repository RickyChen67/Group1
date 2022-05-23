using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    //to add more package spawn locations, add more package prefabs under the package spawner game object

    public GameObject holdingPackage;
    public GameObject exitManager;
    [SerializeField] private bool hasPackage = false;

    void Start()
    {
        //disable all
        for (int x = 0; x < this.transform.childCount; x++)
        {
            this.transform.GetChild(x).gameObject.SetActive(false);
        }

        //pick a random package to enable
        this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject.SetActive(true);

        //hide holding package
        holdingPackage.GetComponent<Renderer>().enabled = false;
    }

    public void UpdatePackage(bool packageStatus)
    {
        hasPackage = packageStatus;

        //update holding package
        holdingPackage.GetComponent<Renderer>().enabled = hasPackage;

        //update exit manager
        exitManager.GetComponent<ExitManager>().UpdateExit(hasPackage);
    }
}
