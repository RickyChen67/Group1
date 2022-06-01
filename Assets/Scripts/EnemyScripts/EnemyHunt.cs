using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHunt : EnemyState
{
    private int huntSpeed = 8;
    private float movingTimer; // The timer used to time the enemy's movement
    private int movingTimerLimit = 2; // 2 seconds

    private int huntTimerLimit = 5; // 5 seconds, indicates the duration needed to be outside of the enemy's line of sight or proximity in order to transition it back into its investigation state
    private int huntingProximity = 5;

    // Used as alternating destination points to avoid excessive staggering of the enemy
    private Transform first;
    private Transform second;
    private bool firstWaypoint;

    public EnemyHunt(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) { }

    // Sets the enemy's speed and acceleration to huntSpeed
    // Sets the first and second variables to the first and second children of the enemy (firstWP and secondWP)
    // Turns the enemy's AutoBraking on to avoid overshooting its destination and sets the destination to the current player's position
    public override void Enter()
    {
        name = State.HUNT;
        first = enemy.transform.GetChild(0);
        second = enemy.transform.GetChild(1);

        SetAgentSpeedAndAcceleration(huntSpeed);
        agent.autoBraking = true;
        agent.SetDestination(player.position);

        base.Enter();
    }

    // Causes the enemy to move towards the player
    public override void Update()
    {
        runTimer();
        runMovingTimer();

        // Checks if the huntTimerLimit has been reached and if the player is not detected
        // If so, transitions the enemy into its EnemyInvestigate state and turns off AutoBraking
        if (timerExceeded() && !playerDetected())
        {
            nextState = new EnemyInvestigate(enemy, agent, player, package);
            agent.autoBraking = false;
            stage = Event.Exit;
        }

        // Checks if the movingTimerLimit has been reached
        // If so, sets the destination of the enemy to the player's position and resets movingTimer to 0
        // If the player is detected, the hunting timer is also reset
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
    
    // Checks if the player is within the enemy's hunting proximity, radius based on huntingProximity
    public bool playerInHuntProximity()
    {
        return (Vector3.Distance(player.position, enemy.transform.position) <= huntingProximity);
    }

    // Checks if the player is either in the line of sight or hunting proximity of the enemy
    protected bool playerDetected()
    {
        return playerInLineOfSight() || playerInHuntProximity();
    }
}
