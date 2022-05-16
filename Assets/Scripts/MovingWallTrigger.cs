using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallTrigger : MonoBehaviour
{
    public bool holdingName;


    public Animation wallAnimation;
    public ArrayList wallAnimationList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        wallAnimation = GetComponent<Animation>();
        foreach (AnimationState state in wallAnimation)
        {
            wallAnimationList.Add(state.name);
        }
    }

    private void Update()
    {
        
    }

    public void playerTriggered()
    {
        if (holdingName)
        {
            // Moves the wall to its designated position
            wallAnimation.Play(wallAnimationList[0].ToString());
            holdingName = false;
        }
        else if (!holdingName)
        {
            // Moves the wall to its original position
            wallAnimation.Play(wallAnimationList[1].ToString());
            holdingName = true;
        }
    }
}
