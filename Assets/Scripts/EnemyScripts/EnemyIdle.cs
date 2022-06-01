using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(GameObject _enemy, NavMeshAgent _agent, Transform _player, MeshRenderer _package) : base(_enemy, _agent, _player, _package) {}

    // Prevents the enemy from moving
    public override void Enter()
    {
        name = State.IDLE;
        agent.isStopped = true;
        base.Enter();
    }

    // Transitions into the EnemyRoam state if the enemy's MeshRender is active, which is controlled in the ExitManager script
    public override void Update()
    {
        if (enemy.GetComponent<MeshRenderer>().isVisible)
        {
            agent.isStopped = false;
            nextState = new EnemyRoam(enemy, agent, player, package);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
