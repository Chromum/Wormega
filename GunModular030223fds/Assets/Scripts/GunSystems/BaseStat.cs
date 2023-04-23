using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Gun/BaseStat")]
public class BaseStat : ScriptableObject
{
    public float FireRateBase;
    public float DamageBase;
    public int MagSizeBase;
    public float AccuracyBase;

}