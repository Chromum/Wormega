using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Actions/Spawnhealers")]

public class SpawnHealers : AIAction
{
    public GameObject HealerPrefab;

    public override void Execute(AIBase AIbase)
    {
        BossAI ai = (BossAI)AIbase.Enemy;
        ai.HealersSpawned = true;
        foreach (var t in ai.HealerPositions)
        {
            ai.currentHealers++;
            GameObject g = GameObject.Instantiate(HealerPrefab, t.transform.position, quaternion.identity);
            g.transform.parent = t;
            g.GetComponent<Healer>().Enable(AIbase.Damageable, AIbase);
            
        }
        AIbase.CurrentState = ai.CurrentResetState;
    }
}
