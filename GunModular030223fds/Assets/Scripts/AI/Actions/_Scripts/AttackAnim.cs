using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Actions/AttackAnim")]
public class AttackAnim : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        Enemy e = AIbase.Enemy;
        if(e.canAttack)
            e.animator.SetBool("AttackAnim", true);
    }
}
