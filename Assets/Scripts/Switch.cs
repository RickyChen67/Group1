using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public MovingWallTrigger wall;

    public bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && inTrigger)
        {
            wall.playerTriggered();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {

        inTrigger = false;
    }
}
