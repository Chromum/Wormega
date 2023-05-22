using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/TurretSee")]
public class TurretSeeEnemy : AIDecision
{
    public LayerMask lm;
    public float radius;
    public float distance;
    public override bool Decide(AIBase AIbase)
    {
        RaycastHit hit;
        return Physics.SphereCast(AIbase.transform.position, radius, AIbase.GetComponent<Turret>().dir.forward, out hit, distance,lm);
    }
}

 
