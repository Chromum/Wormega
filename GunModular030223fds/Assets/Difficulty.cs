using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    public string difficultyName;
    public float Length;
    public Color difficultyColor;
    public Material Mat;
    public EnemyDifficultyStats enemyStats;
}
[System.Serializable]
public struct EnemyDifficultyStats
{
    public float HealthMultiplier;
    public float DamageMultiplier;

    [Header("Grunt Stuff")]
    public GunPart Magazine;
    public GunPart Grip;
    public GunPart Sight;
    public Barrel Barrel;
    public Module Module;
}
