using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class samplePositionTesting : MonoBehaviour
{
    private GameObject enemy;
    private NavMeshAgent agent;
    private Transform player;
    private float timer;
    private float timeLimit = 2f;
    public Transform first;
    public Transform second;
    public bool currentWP;

    private void Start()
    {
        enemy = this.gameObject;
        first = enemy.transform.GetChild(0);
        second = enemy.transform.GetChild(1);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = this.GetComponent<NavMeshAgent>();


        first.position = player.position;
        currentWP = true;
        agent.SetDestination(first.position);
    }

    private void Update()
    {
        runTimer();

        if (timerReached())
        {
            if (currentWP)
            {
                second.position = player.position;
                currentWP = false;
                agent.SetDestination(second.position);
            }
            else
            {
                first.position = player.position;
                currentWP = true;
                agent.SetDestination(first.position);
            }

            resetTimer();
        }
        
    }

    void runTimer()
    {
        timer += Time.deltaTime;
    }

    void resetTimer()
    {
        timer = 0;
    }

    bool timerReached()
    {
        return timer >= timeLimit;
    }
}
