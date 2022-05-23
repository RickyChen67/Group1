using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Creates a class called State that is in charge the Enemy's states
public class State {
    // The states the enemy can be in:
    // Roam: wanders around the maze until it is within a certain distance of the player, goes to proxmity control
    // Proxmity Control: controls the distance of the enemy to remain within a certain distance of the player
    // Hunt: chases after the player until the player is outside of the enemy's line of sight
    public enum STATE
    {
        idle, roam, proximityControl, hunt
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

    public float proximityDistance = 20f;

    float viewDistance = 10f;
    float viewAngle = 70f;

    float timer = 0f;
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
        return (Vector3.Distance(player.position, enemy.transform.position) <= proximityDistance);
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
    }

    public bool timerExceeded()
    {
        return (timer % 60 > 2);
    }
}

public class Idle : State
{
    public Idle(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.idle;
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

public class Roam : State
{
    bool wandering = false;
    Vector3 destination = Vector3.zero;

    public Roam(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.roam;
    }

    // *** Can add animations here to start enemy's roaming state ***
    public override void Enter()
    {
        speedAndAccel = 4f;
        base.Enter();
    }

    public void Wander()
    {
        if (!wandering)
        {
            resetTimer();
            destination = getRandomPosition(20);
            agent.SetDestination(destination);
            if (agent.hasPath)
                wandering = true;

        }
        else if (Vector3.Distance(destination, enemy.transform.position) < 3)
        {
            resetTimer();
            wandering = false;
        }

        agent.SetDestination(destination);
        runTimer();
        if (timerExceeded() && (Vector3.Distance(currentPosition, enemy.transform.position) < 1))
            wandering = false;

    }

    public override void Update()
    {
        // If the player is within the enemy's proximity, then go into proximity control state
        if (playerInProximity())
        {
            nextState = new ProximityControl(enemy, agent, player);
            stage = EVENT.EXIT;
        }
        else
        {
            Wander();
        }
    }

    // *** Reset animation here to prevent issues with animation ***
    public override void Exit()
    {
        base.Exit();
    }
}

public class ProximityControl : State
{
    bool wandering = false;
    Vector3 destination = Vector3.zero;

    public ProximityControl(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.proximityControl;
    }

    // *** Can add animations here for enemy's proximity control state (most likely the same as roaming, maybe add some sounds here too) ***
    public override void Enter()
    {
        speedAndAccel = 5f;
        base.Enter();
    }

    public void controlledWander()
    {
        if (!wandering)
        {
            resetTimer();
            destination = getRandomPosition(10);
            agent.SetDestination(destination);
            if (agent.hasPath && Vector3.Distance(destination, player.position) <= proximityDistance)
                wandering = true;
        }
        else if (Vector3.Distance(destination, enemy.transform.position) < 3)
        {
            resetTimer();
            wandering = false;
        }

        agent.SetDestination(destination);
        runTimer();
        if (timerExceeded() && Vector3.Distance(currentPosition, enemy.transform.position) < 1)
            wandering = false;
    }

    public override void Update()
    {
        if (playerInLineOfSight())
        {
            nextState = new Hunt(enemy, agent, player);
            stage = EVENT.EXIT;
        }
        else
        {
            controlledWander();
        }
    }

    // *** Reset animation here to prevent issues with animation and also stop the audio source ***
    public override void Exit()
    {
        base.Exit();
    }
}

public class Hunt : State
{
    public Hunt(GameObject _enemy, NavMeshAgent _agent, Transform _player) : base(_enemy, _agent, _player)
    {
        name = STATE.hunt;
    }

    // *** Add animations for enemy hunting and probably sound too ***
    public override void Enter()
    {
        speedAndAccel = 9.5f;
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (!playerInLineOfSight())
        {
            nextState = new ProximityControl(enemy, agent, player);
            stage = EVENT.EXIT;
        }
    }

    // *** Reset animation and sound ***
    public override void Exit()
    {
        base.Exit();
    }
}
