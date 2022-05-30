using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject goal;
    public NavMeshAgent agent;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player");
    }
    public bool playerInLineOfSight()
    {
        // Casts a ray in front of the enemy to check if the player is directly in front of it
        RaycastHit target;
        Physics.Raycast(this.transform.position, this.transform.forward, out target, Mathf.Infinity);

        // Vector from the enemy to the player
        Vector3 direction = goal.transform.position - this.transform.position;
        // Angle between the vector above and the direction the enemy is facing
        float angle = Vector3.Angle(direction, this.transform.forward);

        return ((direction.magnitude <= 10 && angle <= 30) || target.collider.gameObject.tag == "Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(this.transform.position, this.transform.forward, Color.red, 60);
            Debug.Log(Vector3.Distance(this.transform.position, goal.transform.position));
            Debug.Log(playerInLineOfSight());
        }

        /*if (!(Vector3.Distance(this.transform.position, goal.transform.position) > 10f))
            agent.speed = 10;
        else
            agent.speed = 3;*/

        // agent.SetDestination(goal.transform.position);

        
        /*
        if (!(Vector3.Distance(this.transform.position, goal.transform.position) < accuracy)){
            Vector3 direction = goal.transform.position - this.transform.position;
            // this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.deltaTime);
            Quaternion lookAtPlayer = Quaternion.LookRotation(goal.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtPlayer, lookSpeed * Time.deltaTime);

            this.transform.Translate(0, 0, speed * Time.deltaTime);

        }
            */
    }
}
