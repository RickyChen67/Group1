using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall_Timer : MonoBehaviour
{

    public bool holdingName;
    public int move = 100;

    private int moveMax;

    public Animation wallAnimation;
    public ArrayList wallAnimationList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        moveMax = move;
        wallAnimation = GetComponent<Animation>();
        foreach (AnimationState state in wallAnimation)
        {
            wallAnimationList.Add(state.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        move -= 1;

        if (move < 0 && holdingName)
        {
            // Moves the wall to its designated position
            wallAnimation.Play(wallAnimationList[0].ToString());
            holdingName = false;
        } else if (move < 0 && !holdingName)
        {
            // Moves the wall to its original position
            wallAnimation.Play(wallAnimationList[1].ToString());
            holdingName = true;
        }

        

        if (move < 0)
        {
            move = moveMax;
        }
    }
}
