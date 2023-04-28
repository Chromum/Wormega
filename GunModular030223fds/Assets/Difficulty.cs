using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    public string difficultyName;
    public float Length;
    public Color difficultyColor;
    public List<EnemyDifficultyStats> enemyStats;
}

public struct EnemyDifficultyStats
{

}
