using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHunt : EnemyState
{
    private int huntSpeed = 8;
    private float movingTimer;
    private int movingTimerLimit = 2;

    private int huntTimerLimit = 5;
    private int huntingProximity = 5;

    private Transform first;
    private Transform second;
    private bool firstWaypoint;

    public EnemyHunt(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    public override void Enter()
    {
        name = State.HUNT;
        first = enemy.transform.GetChild(0);
        second = enemy.transform.GetChild(1);

        SetAgentSpeedAndAcceleration(huntSpeed);
        first.position = player.position;
        firstWaypoint = true;
        agent.SetDestination(first.position);

        base.Enter();
    }

    public override void Update()
    {
        runTimer();
        runMovingTimer();

        if (timerExceeded() && !playerDetected())
        {
            nextState = new EnemyInvestigate(enemy, agent, player, package);
            stage = Event.Exit;
        }


        if (movingTimerExceeded())
        {
            if (firstWaypoint)
            {
                second.position = player.position;
                firstWaypoint = false;
                agent.SetDestination(second.position);
            }
            else
            {
                first.position = player.position;
                firstWaypoint = true;
                agent.SetDestination(first.position);
            }

            resetMovingTimer();

            if (playerDetected())
                resetTimer();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected void runMovingTimer()
    {
        movingTimer += Time.deltaTime;
    }

    protected bool movingTimerExceeded()
    {
        return movingTimer >= movingTimerLimit;
    }

    protected override bool timerExceeded()
    {
        return timer >= huntTimerLimit;
    }

    protected void resetMovingTimer()
    {
        movingTimer = 0;
    }

    protected void resetTimer()
    {
        timer = 0;
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
