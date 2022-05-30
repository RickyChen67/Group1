using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHunt : EnemyState
{
    private int huntSpeed = 8;
    private int movingTimerLimit = 2;
    private int huntTimerLimit = 5;
    private int huntingProximity = 5;

    public EnemyHunt(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    public override void Enter()
    {
        name = State.HUNT;
        Debug.Log(name);
        SetAgentSpeedAndAcceleration(huntSpeed);
        enemy.transform.Translate(player.position);
        base.Enter();
    }

    public override void Update()
    {
        runTimer();

        /*
        if (timerExceeded() && playerDetected())
        {
            agent.SetDestination(player.position);
            resetTimer();
        }

        else if (!playerInLineOfSight() && huntTimerExceeded())
        {
            nextState = new Investigate(enemy, agent, player);
            stage = EVENT.EXIT;
        } */
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected bool movingTimerExceeded()
    {
        return timer >= movingTimerLimit;
    }
    
    protected override bool timerExceeded()
    {
        return timer >= huntTimerLimit;
    }

    public bool playerInHuntProximity()
    {
        return (Vector3.Distance(player.position, enemy.transform.position) <= huntingProximity);
    }

    protected bool playerDetected()
    {
        return playerInLineOfSight() || playerInHuntProximity();
    }
}
