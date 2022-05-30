using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInvestigate : EnemyState
{
    private int investSpeed = 6;
    private int investDistance = 5;
    private int investTimerLimit = 120;

    public EnemyInvestigate(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    public override void Enter()
    {
        name = State.INVESTIGATE;
        Debug.Log(name);
        SetAgentSpeedAndAcceleration(investSpeed);
        base.Enter();
    }

    public override void Update()
    {
        runTimer();

        if (!isMoving)
        {
            destination = getInvestPosition();
            agent.SetDestination(destination);
            isMoving = true;
        }
        else
        {
            if (Vector3.Distance(player.position, destination) > investigationDistance || (agent.remainingDistance <= 1 && agent.remainingDistance > 0))
                isMoving = false;
        }

        if (playerInLineOfSight() || timerExceeded())
        {
            nextState = new EnemyHunt(enemy, agent, player, package);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private Vector3 getInvestPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * investDistance;
        randomDirection += player.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, investDistance, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    protected override bool timerExceeded()
    {
        return timer >= investTimerLimit;
    }
}
