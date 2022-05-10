using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall_Timer : MonoBehaviour
{

    public bool holdingName;
    public int move = 100;

    private int moveMax;

    public Animation testing;

    // Start is called before the first frame update
    void Start()
    {
        moveMax = move;
    }

    // Update is called once per frame
    void Update()
    {
        move -= 1;

        if (move < 0 && holdingName)
        {
            testing.Play("Demo Animation");
            holdingName = false;
        } else if (move < 0 && !holdingName)
        {
            testing.Play("New Animation");
            holdingName = true;
        }

        

        if (move < 0)
        {
            move = moveMax;
        }
    }
}
