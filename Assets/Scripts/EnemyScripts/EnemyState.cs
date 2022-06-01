using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    // IDLE: stops the enemy from moving until it successfully spawns at an exit
    // ROAM: wanders around the maze until the player is within investigation distance, the timer has exceeded roaming time, or the package has been obtained
    // INVESTIGATE: wanders within the investigation distance of the player until the player is within its line of sight or the timer has exceeded investigation time
    // HUNT: moves towards the player until they are out of the enemy's line of sight or hunting proximity for a certain amount of time
    public enum State
    {
        IDLE, ROAM, INVESTIGATE, HUNT
    }

    public enum Event
    {
        Enter, Update, Exit
    };

    public State name; // The enemy's current state
    protected Event stage; // The current state's event (from enum Event)
    protected EnemyState nextState; // The state being transitioned into, i.e. ROAM -> INVESTIGATE, INVESTIGATE -> HUNT, etc.
    
    protected GameObject enemy; // The enemy (tagged "Enemy"), must contain a NavMeshAgent as a component
    protected NavMeshAgent agent; // The enemy's NavMeshAgent
    protected Transform player; // The transform of the player (tagged "Player")
    protected MeshRenderer package; // The package attached to the player (tagged "HoldingPackage")

    protected Vector3 destination; // The enemy's current destination
    protected bool isMoving = false;

    protected int investigationDistance = 15;
    protected int viewDistance = 10;
    protected int viewAngle = 80;

    public EnemyState(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package)
    {
        enemy = _enemy;
        agent = _agent;
        player = _player;
        package = _package;
        stage = Event.Enter;
    }

    // Initial function that runs when changing states
    public virtual void Enter() { stage = Event.Update; }

    // The function that runs until a new state is declared
    public virtual void Update() { stage = Event.Update; }

    // The function that runs after a new state is declared to end the current state
    public virtual void Exit() { stage = Event.Exit; }

    // The function to run when using this (EnemyState) class
    public EnemyState Process()
    {
        if (stage == Event.Enter) Enter();
        if (stage == Event.Update) Update();
        if (stage == Event.Exit)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    // Checks if the enemy is looking at the player or the player is within the enemy's view cone
    protected bool playerInLineOfSight()
    {
        // Casts a ray in front of the enemy to check if the player is directly in front of it
        RaycastHit target;
        Physics.Raycast(enemy.transform.position, enemy.transform.forward, out target, Mathf.Infinity);

        // The vector from the enemy to the player and the angle between the given vector and the direction the enemy is facing
        Vector3 direction = player.position - enemy.transform.position;
        float angle = Vector3.Angle(direction, enemy.transform.forward);

        // If the player is within viewing distance or in front of the enemy, return true else false
        return ((direction.magnitude <= viewDistance && angle <= viewAngle) || target.collider.gameObject.tag == "Player");
    }

    // Sets the enemy's speed and acceleration
    protected void SetAgentSpeedAndAcceleration(int speed)
    {
        agent.speed = speed;
        agent.acceleration = speed * 2;
    }

    // A global timer
    protected float timer;
    protected void runTimer()
    {
        timer += Time.deltaTime;
        // Debug.Log(timer);
    }

    protected virtual bool timerExceeded() { return false; }
}
