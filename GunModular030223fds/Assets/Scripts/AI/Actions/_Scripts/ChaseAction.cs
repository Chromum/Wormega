using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "Sam Green/AI/Actions/ChaseAction")]
public class ChaseAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        var navmesh = AIbase.GetComponent<NavMeshAgent>();
        navmesh.isStopped = false;
        Enemy e = AIbase.GetComponent<Enemy>();
        navmesh.SetDestination(e.player.transform.position);
    }
}
