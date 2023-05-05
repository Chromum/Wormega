using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/NeedsToHeal")]
public class NeedsToHealDecisions : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        BossAI ai = (BossAI)AIbase.Enemy;
        if (ai.healerSpawnCooldown.HasFinished())
        {
            if (AIbase.Damageable.Health <= (AIbase.Damageable.MaxHealth / 2))
            {
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }
}
