using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/ReloadDecision")]
public class ReloadDecision : AIDecision
{

    public override bool Decide(AIBase AIbase)
    {
        Grunt g = (Grunt)AIbase.Enemy;
        return !g.AIGun.NeedsToReload();
    }
}
