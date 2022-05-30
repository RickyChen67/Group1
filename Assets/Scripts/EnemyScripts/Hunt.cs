using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunt : State
{
    public Hunt(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.HUNT;
    }

    // *** Add animations for enemy hunting and probably sound too ***
    public override void Enter()
    {
        speedAndAccel = 8f;
        resetTimer();
        agent.SetDestination(player.position);
        runTimer();
        base.Enter();
    }

    public override void Update()
    {
        /*if (timerExceeded() && (playerInLineOfSight() || playerInHuntProximity()))
        {
            resetTimer();
            agent.SetDestination(player.position);
        }

        else if (!playerInLineOfSight() && huntTimerExceeded())
        {
            nextState = new Investigate(enemy, agent, player);
            stage = EVENT.EXIT;
        }

        runTimer();*/
    }

    // *** Reset animation and sound ***
    public override void Exit()
    {
        base.Exit();
    }
}