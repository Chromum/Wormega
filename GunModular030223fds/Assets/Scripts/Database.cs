using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Create New Database")]
public class Database : ScriptableObject
{
    public List<GunPart> gunParts = new List<GunPart>();
    [SerializeField] public List<GunPart> Common, Rare, Legendary, Mythic = new List<GunPart>();
    public List<Item> items = new List<Item>();
    [SerializeField] public List<Item> CommonUp, RareUp, LegendaryUp, MythicUp = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    [Button("Fill Database")]
    public void FillInDatabase()
    {
        gunParts.Clear();
        items.Clear();
        Common.Clear();
        CommonUp.Clear();
        Rare.Clear();
        RareUp.Clear();
        Legendary.Clear();
        LegendaryUp.Clear();
        Mythic.Clear();
        MythicUp.Clear();
        // Find all GunPart ScriptableObjects in the project and add them to the list
        string[] guids = AssetDatabase.FindAssets("t:GunPart");
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GunPart gunPart = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GunPart)) as GunPart;
            if (gunPart != null)
            {
                if (!gunPart.Debug)
                {
                    gunParts.Add(gunPart);
                    switch(gunPart.Rareity)
                    {
                        case Rareity.Common:
                            Common.Add(gunPart);
                            break;
                        case Rareity.Rare:
                            Rare.Add(gunPart);
                            break;
                        case Rareity.Legendary:
                            Legendary.Add(gunPart);
                            break;
                        case Rareity.Mythic:
                            Mythic.Add(gunPart);
                            break;
                    }
                }
            }
        }

        // Find all GunPart ScriptableObjects in the project and add them to the list
        string[] guids2 = AssetDatabase.FindAssets("t:Item");
        foreach (string guid in guids2)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Item gunPart = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Item)) as Item;
            if (gunPart != null)
            {
                if (!gunPart.Debug)
                {
                    items.Add(gunPart);
                    switch (gunPart.Rareity)
                    {
                        case Rareity.Common:
                            CommonUp.Add(gunPart);
                            break;
                        case Rareity.Rare:
                            RareUp.Add(gunPart);
                            break;
                        case Rareity.Legendary:
                            LegendaryUp.Add(gunPart);
                            break;
                        case Rareity.Mythic:
                            MythicUp.Add(gunPart);
                            break;
                    }
                }
            }
        }
    }
}




