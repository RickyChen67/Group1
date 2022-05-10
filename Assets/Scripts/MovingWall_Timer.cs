using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall_Timer : MonoBehaviour
{

    public bool holdingName;
    public int move = 100;

    private int moveMax;

    public Animation testing;
    public ArrayList lists = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        moveMax = move;
        testing = GetComponent<Animation>();
        foreach (AnimationState state in testing)
        {
            lists.Add(state.name);
        }
        Debug.Log(lists[0]);
        Debug.Log(lists[1]);
    }

    // Update is called once per frame
    void Update()
    {
        move -= 1;

        if (move < 0 && holdingName)
        {
            testing.Play(lists[0].ToString());
            holdingName = false;
        } else if (move < 0 && !holdingName)
        {
            testing.Play(lists[1].ToString());
            holdingName = true;
        }

        

        if (move < 0)
        {
            move = moveMax;
        }
    }
}
