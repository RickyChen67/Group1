using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRandomizer : MonoBehaviour
{
    public Mesh mesh01;
    public Mesh mesh02;
    public Mesh mesh03;

    public bool randomizeRotation = true;

    void Start()
    {
        Mesh[] meshArray = new Mesh[3];
        meshArray[0] = mesh01;
        meshArray[1] = mesh02;
        meshArray[2] = mesh03;

        this.GetComponent<MeshFilter>().mesh = meshArray[Random.Range(0, 3)];

        if (randomizeRotation == true)
        {
            this.transform.Rotate(0, (90 * Random.Range(0, 4)), 0);
        }
    }
}
