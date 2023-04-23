using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/CanAttack")]
public class CanAttack : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        if (AIbase.GetComponent<Enemy>().canAttack)
            return true;
        else return false;
    }
}
