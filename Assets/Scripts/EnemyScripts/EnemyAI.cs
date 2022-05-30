using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    EnemyState currentState;
    public MeshRenderer package;
    public bool hasPackage;

    void Start()
    {
        // Gets the variables needed for the State class and sets current state to begin with the Roam state
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        package = GameObject.FindGameObjectWithTag("HoldingPackage").GetComponent<MeshRenderer>();

        currentState = new EnemyIdle(this.gameObject, agent, player.transform, package);
    }

    void Update()
    {
        // Sets the current state to the state return by the State class
        currentState = currentState.Process();
        Debug.Log(currentState.name);
    }
}
