using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Actions/ReloadAction")]
public class ReloadAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        Grunt g = (Grunt)AIbase.Enemy;
        if(g.AIGun.NeedsToReload())
            g.AIGun.Reload();
        
    }
}
