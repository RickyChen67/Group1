using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoam : EnemyState
{
    private int roamingSpeed = 4;
    private int roamTimerLimit = 60; // 60 seconds
    private int roamingDistance = 40;

    // The dimensions of the maze
    private int mapWidth = 48;
    private int mapHeight = 48;

    // The current quadrant of the enemy's destination (Top Right: Q1, Bottom Right: Q2, Top Left: Q3, Bottom Left: Q4)
    private string quadrant;

    public EnemyRoam(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    // Sets the current speed and acceleration to roamingSpeed
    public override void Enter()
    {
        name = State.ROAM;
        SetAgentSpeedAndAcceleration(roamingSpeed);
        base.Enter();
    }

    // Causes the enemy to wander around the maze until a certain condition is fulfilled
    public override void Update()
    {
        runTimer();
        
        // Determines whether the enemy has a set destination, if not then a new destination is set
        if (!isMoving)
        {
            // Gets a random position in the nav mesh and the position's quadrant
            destination = getRoamPosition();
            quadrant = getCurrentQuadrant(destination);

            // Sets the enemy's destination to destination
            agent.SetDestination(destination);
            isMoving = true;
        }
        else
        {
            // If the enemy reaches or is near the set destination, a new destination should be set
            if (agent.remainingDistance < 2 && agent.remainingDistance > 0)
                isMoving = false;
        }

        // Checks if either a minute has passed, the player obtained the package, is within investigation distance or is within the enemy's line of sight
        // If so, the enemy transitions into its investigation state
        if (timerExceeded() || package.enabled || isWithinInvestigationDistance() || playerInLineOfSight())
        {
            nextState = new EnemyInvestigate(enemy, agent, player, package);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    // Returns a random position in the NavMesh within a radius equal to roamingDistance and is not in the same quadrant as the destination
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

    // Returns the quadrant of the given position (Top Right: Q1, Bottom Right: Q2, Top Left: Q3, Bottom Left: Q4)
    private string getCurrentQuadrant(Vector3 position)
    {
        if (position.x >= mapHeight/2)
        {
            if (position.z >= mapWidth/2)
                return "Q1";
            else
                return "Q2";
        }
        else
        {
            if (position.z >= mapWidth/2)
                return "Q3";
            else
                return "Q4";
        }
    }

    // Checks if the timer is greater than or equal to roamTimerLimit (1 minute)
    protected override bool timerExceeded()
    {
        return timer >= roamTimerLimit;
    }

    // Checks if the distance between the enemy and player is less than or equal to investigationDistance
    private bool isWithinInvestigationDistance()
    {
        return Vector3.Distance(enemy.transform.position, player.position) <= investigationDistance;
    }
}
