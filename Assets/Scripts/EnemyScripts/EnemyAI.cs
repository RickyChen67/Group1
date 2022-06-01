using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private MeshRenderer package;
    private EnemyState currentState;

    void Start()
    {
        // Gets the variables needed for the State class and sets the current state to begin with the EnemyIdle state
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Gets the HoldingPackage attached to the player
        package = GameObject.FindGameObjectWithTag("HoldingPackage").GetComponent<MeshRenderer>();

        currentState = new EnemyIdle(this.gameObject, agent, player.transform, package);
    }

    void Update()
    {
        // Sets the current state to the state returned by the EnemyState class
        currentState = currentState.Process();
        Debug.Log(currentState.name);
    }
}
