using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Sam Green/AI/Actions/BallerFollowAction")]
public class BallerFollow : AIAction
{
    public bool moveaway;
    public float followDistance = 5f;
    public float moveAwayDistance = 2f;
    private float sqrFollowDistance;
    private float sqrMoveAwayDistance;
    public override void Execute(AIBase AIbase)
    {
        AIbase.Enemy.NavMeshAgent.isStopped = false;
        sqrFollowDistance = followDistance * followDistance;
        sqrMoveAwayDistance = moveAwayDistance * moveAwayDistance;
        NavMeshAgent agent = AIbase.Enemy.NavMeshAgent;
        Transform player = AIbase.Enemy.player.transform;

        Vector3 offset = player.position - AIbase.transform.position;
        float sqrDistanceToPlayer = offset.sqrMagnitude;


    
            if (sqrDistanceToPlayer >= sqrFollowDistance)
            {
                agent.SetDestination(player.position);
            }
            else if (sqrDistanceToPlayer <= sqrMoveAwayDistance)
            {
                Vector3 moveAwayDirection = (AIbase.transform.position - player.position).normalized;
                Vector3 moveAwayTarget = AIbase.transform.position + moveAwayDirection * followDistance;
                agent.SetDestination(moveAwayTarget);
                Debug.Log("TEEE");
            }
            else
            {
                agent.ResetPath();
                Debug.Log("HA");
            }
        
    }
}
