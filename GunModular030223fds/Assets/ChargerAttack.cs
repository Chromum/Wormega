using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wormega/Attacks/ChargerAttack")]
public class ChargerAttack : Attack
{
    public float minDamage;
    public float maxDamage;
    public float minKnockbackForce = 100f;
    public float maxKnockbackForce = 500f;
    public override void Execute(Enemy en)
    {
        
        base.Execute(en);
        HornedEnemy e = en as HornedEnemy;
        e.distanceFromPlayerStart = Vector3.Distance(en.NavMeshAgent.transform.position, en.player.transform.position);
        Vector3 chargeDirection = (en.player.transform.position - en.NavMeshAgent.transform.position).normalized;
        en.NavMeshAgent.velocity = chargeDirection * en.NavMeshAgent.speed;
        e.hasCharged = true;
    }
}
