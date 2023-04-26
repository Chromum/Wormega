using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rareity")]
public class RareityItem : ScriptableObject
{
    public Rareity Rareity;
    public float PercChance;
    public Material glowMaterial;
    public Material HoloMaterial;
}
