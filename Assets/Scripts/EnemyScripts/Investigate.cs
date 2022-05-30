using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Investigate : State
{
    bool wandering = false;
    Vector3 destination = Vector3.zero;

    public Investigate(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.INVESTIGATE;
    }

    // *** Can add animations here for enemy's proximity control state (most likely the same as roaming, maybe add some sounds here too) ***
    public override void Enter()
    {
        speedAndAccel = 5f;
        base.Enter();
    }

    public void controlledWander()
    {
        if (!wandering)
        {
            resetTimer();
            destination = getRandomPosition(10);
            agent.SetDestination(destination);
            if (agent.hasPath && Vector3.Distance(destination, player.position) <= investigateDistance)
                wandering = true;
        }
        else if (Vector3.Distance(destination, enemy.transform.position) < 3)
        {
            resetTimer();
            wandering = false;
        }

        agent.SetDestination(destination);
        runTimer();
        if (timerExceeded() && Vector3.Distance(currentPosition, enemy.transform.position) < 1)
            wandering = false;
    }

    public override void Update()
    {
        if (playerInLineOfSight())
        {
            nextState = new Hunt(enemy, agent, player);
            stage = EVENT.EXIT;
        }
        else
        {
            controlledWander();
        }
    }

    // *** Reset animation here to prevent issues with animation and also stop the audio source ***
    public override void Exit()
    {
        base.Exit();
    }
}
