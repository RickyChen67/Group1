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

    public State name;
    protected Event stage;
    protected EnemyState nextState;
    
    protected GameObject enemy;
    protected NavMeshAgent agent;
    protected Transform player;
    protected MeshRenderer package;

    protected Vector3 destination;
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

    public virtual void Enter() { stage = Event.Update; }

    public virtual void Update() { stage = Event.Update; }

    public virtual void Exit() { stage = Event.Exit; }

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

    public bool playerInLineOfSight()
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

    protected void SetAgentSpeedAndAcceleration(int speed)
    {
        agent.speed = speed;
        agent.acceleration = speed * 2;
    }

    protected float timer;
    protected void runTimer()
    {
        timer += Time.deltaTime;
        // Debug.Log(timer);
    }

    protected void resetTimer()
    {
        timer = 0;
    }

    protected virtual bool timerExceeded() { return false; }
}
