using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Creates a class called State that is in charge the Enemy's states
public class State {
    // The states the enemy can be in:
    // IDLE: Prevents the enemy from moving until it has spawned in one of the exits
    // ROAM: Wanders around the maze until it is within a certain distance of the player, then goes into INVESTIGATE
    // INVESTIGATE: Wanders around the maze, while remaining within a certain distance of the player
    // HUNT: Chases after the player until the player is outside of the enemy's line of sight or hunting proximity
    public enum STATE
    {
        IDLE, ROAM, INVESTIGATE, HUNT
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected State nextState;
    protected GameObject enemy;
    protected Transform player;
    protected NavMeshAgent agent;
    public float speedAndAccel;

    public float investigateDistance = 20f;

    float viewDistance = 10f;
    float viewAngle = 60f;

    float timer = 0f;
    float timerLimit = 2f;
    float huntTimerLimit = 4f;
    public Vector3 currentPosition;

    // Constructor
    public State(GameObject _enemy, NavMeshAgent _agent, Transform _player)
    {
        enemy = _enemy;
        agent = _agent;
        player = _player;
        stage = EVENT.ENTER;
    }

    // The base functions used to process the current event
    public virtual void Enter() {
        agent.speed = speedAndAccel;
        agent.acceleration = speedAndAccel;
        stage = EVENT.UPDATE; 
    }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            // Returns the nextState the enemy should transition into once certain conditions are met
            return nextState;
        }
        // Returns the current state if no nextState is selected
        return this;
    }

    // Checks if the player is within the proximityDistance of the enemy
    public bool playerInProximity()
    {
        return (Vector3.Distance(player.position, enemy.transform.position) <= investigateDistance);
    }

    // Checks if the player is within the proximityDistance/2 of the enemy
    public bool playerInHuntProximity()
    {
        return (Vector3.Distance(player.position, enemy.transform.position) <= investigateDistance / 2);
    }

    // Checks if the player is in the enemy's line of sight
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

    // Finds a random position on the Nav Mesh and returns the position as a Vector3, also sets the current position of the enemy
    public Vector3 getRandomPosition(float radius)
    {
        currentPosition = enemy.transform.position;
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += enemy.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    
    public void resetTimer()
    {
        timer = 0;
    }

    public void runTimer()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
    }

    public bool timerExceeded()
    {
        return (timer % 60 > timerLimit);
    }

    public bool huntTimerExceeded()
    {
        return (timer % 60 > huntTimerLimit);
    }
}

public class Idle : State
{
    public Idle(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        speedAndAccel = 0f;
        base.Enter();
    }

    public override void Update()
    {
        if (enemy.GetComponent<MeshRenderer>().isVisible)
        {
            nextState = new Roam(enemy, agent, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
