using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public Damageable HealerDamageable;
    public Damageable Target;
    public LineRenderer Lr;
    private BossAI ai;
    private Transform ta;
    public void Start()
    {
    }

    public void Update()
    {
        Lr.SetPosition(0,transform.position);
        Lr.SetPosition(1,ta.transform.position);
    }
    
    public void Enable(Damageable t, AIBase r)
    {
        Target = t;
        Lr.positionCount = 2;
        ta = r.transform;
        ai = (BossAI)r.Enemy;
        HealerDamageable.Death += ai.HealerDied;
    }

    
    
}
