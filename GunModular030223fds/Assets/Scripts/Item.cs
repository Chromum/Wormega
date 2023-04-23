using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Rareity { Common, Rare, Legendary }

[CreateAssetMenu(menuName = "Wormega/ItemSystem/Item")]
public class Item : ScriptableObject
{
    public bool Debug;
    public Rareity Rareity;
    public Stats ItemStats;
}
