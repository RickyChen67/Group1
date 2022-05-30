using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoam : EnemyState
{
    private int roamingSpeed = 4;
    private int roamTimerLimit = 60;
    private int roamingDistance = 40;

    private string quadrant;

    public EnemyRoam(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    public override void Enter()
    {
        name = State.ROAM;
        SetAgentSpeedAndAcceleration(roamingSpeed);
        base.Enter();
    }

    public override void Update()
    {
        runTimer();
        
        if (!isMoving)
        {
            destination = getRoamPosition();
            quadrant = getCurrentQuadrant(destination);
            agent.SetDestination(destination);
            isMoving = true;
        }
        else
        {
            if (agent.remainingDistance < 2 && agent.remainingDistance > 0)
                isMoving = false;
        }

        if (timerExceeded() || package.enabled || isWithinInvestigationDistance())
        {
            nextState = new EnemyInvestigate(enemy, agent, player, package);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private Vector3 getRoamPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamingDistance;
        randomDirection += enemy.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamingDistance, 1) && quadrant != getCurrentQuadrant(hit.position))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private string getCurrentQuadrant(Vector3 position)
    {
        if (position.x >= 24)
        {
            if (position.z >= 24)
                return "Q1";
            else
                return "Q2";
        }
        else
        {
            if (position.z >= 24)
                return "Q3";
            else
                return "Q4";
        }
    }

    protected override bool timerExceeded()
    {
        return timer >= roamTimerLimit;
    }

    private bool isWithinInvestigationDistance()
    {
        return Vector3.Distance(enemy.transform.position, player.position) <= investigationDistance;
    }
}
