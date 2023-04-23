using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Actions/AttackAction")]
public class AttackAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        Enemy e = AIbase.GetComponent<Enemy>();
        if (e.animator != null)
        {
            if (e.animator.GetBool("AttackAnim") == false && e.canAttack)
            {
                e.animator.SetBool("AttackAnim", true);
                if(e.NavMeshAgent == true)
                    e.NavMeshAgent.isStopped = true;
            }
        }


    }
}
