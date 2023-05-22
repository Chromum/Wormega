using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wormega/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float Health;
    public float minAttackDamage;
    public float maxAttackDamage;
    public float attackCooldown;
}
