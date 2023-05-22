using System;
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
    }

    public void OnEnable()
    {
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
        DifficultyStats = GameManager.instance.currentDifficulty.enemyStats;
        AIGun.Barrel = DifficultyStats.Barrel;
        AIGun.Grip = DifficultyStats.Barrel;
        AIGun.Magazine = DifficultyStats.Magazine;
        AIGun.Sight = DifficultyStats.Sight;
        AIGun.Module = DifficultyStats.Module;
        AIGun.RecalculateStats();
    }
}
