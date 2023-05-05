using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/HasSpawnedHealers")]
public class HealersSpawned : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        BossAI ai = (BossAI)AIbase.Enemy;
        if (ai.HealersSpawned)
            return false;
        else return true;
    }
}
