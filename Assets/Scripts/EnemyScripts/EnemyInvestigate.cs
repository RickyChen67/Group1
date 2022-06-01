using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInvestigate : EnemyState
{
    private int investSpeed = 6;
    private int investDistance = 5;
    private int investTimerLimit = 120; // 120 seconds

    public EnemyInvestigate(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    // Sets the enemy's speed and acceleration to investSpeed
    public override void Enter()
    {
        name = State.INVESTIGATE;
        SetAgentSpeedAndAcceleration(investSpeed);
        base.Enter();
    }

    // Causes the enemy to wander close to the player until certain conditions are fulfilled
    public override void Update()
    {
        runTimer();

        // Checks if the enemy has a destination to go to, if not then sets a new destination
        if (!isMoving)
        {
            destination = getInvestPosition();
            agent.SetDestination(destination);
            isMoving = true;
        }
        else
        {
            // Checks if the distance between the player and the destination is greater than the investigationDistance or the enemy is near or has reached the destination
            // If one of the two conditions are true, a new destination should be set
            if (Vector3.Distance(player.position, destination) > investigationDistance || (agent.remainingDistance <= 2 && agent.remainingDistance > 0))
                isMoving = false;
        }

        // Checks if the player is within the enemy's line of sight or 2 minutes have passed
        // If so, the enemy transitions into its hunting state
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

    // Returns a random position in the NavMesh that is within investDistance of the player
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

    // Checks if the timer is greater than or equal to investTimerLimit
    protected override bool timerExceeded()
    {
        return timer >= investTimerLimit;
    }
}
