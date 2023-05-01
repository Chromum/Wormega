using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grunt : Enemy
{
    public Gun AIGun;

    public void Start()
    {
        base.Start();
        ResetStats();
    }

    public void Update()
    {
        base.Update();
        if(cooldown != AIGun.FireRate)
            cooldown = AIGun.FireRate;
    }

    public void ResetStats()
    {
        AIGun.Barrel = Stats.Barrel;
        AIGun.Grip = Stats.Barrel;
        AIGun.Magazine = Stats.Magazine;
        AIGun.Sight = Stats.Sight;
        AIGun.Module = Stats.Module;
        AIGun.RecalculateStats();
    }
}
