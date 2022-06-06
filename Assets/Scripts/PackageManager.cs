using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    //to add more package spawn locations, add more package prefabs under the package spawner game object

    public GameObject holdingPackage;
    public GameObject exitManager;
    [SerializeField] private bool hasPackage = false;

    private GameObject activePackage;
    public AudioSource packageSound;
    public List<AudioClip> packageSounds = new List<AudioClip>(2);

    void Start()
    {
        //disable all
        for (int x = 0; x < this.transform.childCount; x++)
        {
            this.transform.GetChild(x).gameObject.SetActive(false);
        }

        activePackage = this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject;
        packageSound = activePackage.GetComponent<AudioSource>();
        packageSound.clip = packageSounds[0];
        packageSound.Play(0);
         
        //pick a random package to enable
        activePackage.SetActive(true);

        //hide holding package
        holdingPackage.GetComponent<Renderer>().enabled = false;
    }

    public void UpdatePackage(bool packageStatus)
    {
        packageSound.loop = false;
        packageSound.clip = packageSounds[1];
        packageSound.Play(0);

        hasPackage = packageStatus;

        //update holding package
        holdingPackage.GetComponent<Renderer>().enabled = hasPackage;

        //update exit manager
        exitManager.GetComponent<ExitManager>().UpdateExit(hasPackage);
    }
}
