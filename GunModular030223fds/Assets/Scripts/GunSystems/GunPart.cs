using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wormega/Gun/GunPart")]
public class GunPart : ScriptableObject
{
    [Header("Item Information")] 
    public string Name;

    public string Bio;
    [Header("Stats")]
    public float FireRate;
    public float Damage;
    public int MagSize;
    public float Accuracy;

    [Header("Outsider Info")] 
    public Sprite Sprite;
    public Rareity Rareity;
    public bool Debug;
}

