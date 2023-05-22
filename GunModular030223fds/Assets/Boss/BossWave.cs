using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/BossWave")]
public class BossWave : ScriptableObject
{
    public float bossHealth;
    public List<AttackAndCooldown> bossAttacks;
    public int enemysToSpawn; 
    public List<EnemySpawnData> enemySpawnArray;
    
}

[System.Serializable]
public class EnemySpawnData
{
    public Poolee enemyType;
    public float chanceOfSpawning;
}

