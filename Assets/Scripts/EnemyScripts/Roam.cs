using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roam : State
{
    bool wandering = false;
    Vector3 destination = Vector3.zero;

    public Roam(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.ROAM;
    }

    // *** Can add animations here to start enemy's roaming state ***
    public override void Enter()
    {
        speedAndAccel = 4f;
        base.Enter();
    }

    public void Wander()
    {
        if (!wandering)
        {
            resetTimer();
            destination = getRandomPosition(20);
            agent.SetDestination(destination);
            if (agent.hasPath)
                wandering = true;
        }
        else if (Vector3.Distance(destination, enemy.transform.position) < 3)
        {
            resetTimer();
            wandering = false;
        }

        agent.SetDestination(destination);
        runTimer();
        if (!agent.hasPath)
            wandering = false;

        if (timerExceeded() && (Vector3.Distance(currentPosition, enemy.transform.position) < 1))
            wandering = false;

    }

    public override void Update()
    {
        // If the player is within the enemy's proximity, then go into proximity control state
        if (playerInProximity())
        {
            nextState = new Investigate(enemy, agent, player);
            stage = EVENT.EXIT;
        }
        else
        {
            Wander();
        }
    }

    // *** Reset animation here to prevent issues with animation ***
    public override void Exit()
    {
        base.Exit();
    }
}
