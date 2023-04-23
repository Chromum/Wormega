using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/CloseToPlayer")]
public class IsCloseToPlayer : AIDecision
{
    
    public override bool Decide(AIBase AIbase)
    {
        Enemy e = AIbase.Enemy;

        if (Vector3.Distance(AIbase.transform.position, e.player.transform.position) <= e.Attack.AttackDistance) return true;
        else return false;
    }
}
