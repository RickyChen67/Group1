using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingWallRandomTimer : MonoBehaviour
{
    public bool holdingName;
    public int move = 100;

    private int moveMax;

    public Animation wallAnimation;
    //public ArrayList wallAnimationList = new ArrayList();

    private System.Random random = new System.Random();
    public int ranMin;
    public int ranMax;

    // Start is called before the first frame update
    void Start()
    {
        moveMax = move;
        /*wallAnimation = GetComponent<Animation>();
        foreach (AnimationState state in wallAnimation)
        {
            wallAnimationList.Add(state.name);
            Debug.Log(state.name);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        move -= 1;

        if (move < 0 && holdingName)
        {
            // Moves the wall to its designated position
            wallAnimation.Play("1");

            holdingName = false;
        }
        else if (move < 0 && !holdingName)
        {
            // Moves the wall to its original position
            wallAnimation.Play("2");
            holdingName = true;
        }



        if (move < 0)
        {
            Debug.Log("Move should trigger");
            Debug.Log(moveMax);
            moveMax = random.Next(ranMin, ranMax);
            move = moveMax;
        }
    }
}
