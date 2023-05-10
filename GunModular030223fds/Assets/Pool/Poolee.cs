using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/Pool System/Poolee")]
public class Poolee : ScriptableObject
{
    public PoolType poolType;
    public int Amount;
    public GameObject poolPrefab;
}


