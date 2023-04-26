using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ItemBox : MonoBehaviour
{
    public List<GlowStruct> glowObjects = new List<GlowStruct>();
    public List<GlowStruct> hologramObjects = new List<GlowStruct>();

    public RareityItem Common, Rare, Legendary;

    public Transform Items;

    [Button]
    public void OpenBox()
    {

    }


    public void SetType(Rareity rareity)
    {
        Material matGlow = null;
        Material matHolo = null;
        switch (rareity)
        {
            case Rareity.Common:
                matGlow = Common.glowMaterial;
                matHolo = Common.HoloMaterial;
                break;
            case Rareity.Rare:
                matGlow = Rare.glowMaterial;
                matHolo = Rare.HoloMaterial;
                break;
            case Rareity.Legendary:
                matGlow = Legendary.glowMaterial;
                matHolo = Legendary.HoloMaterial;
                break;
            default:
                break;
        }
        
        foreach(GlowStruct glow in glowObjects)
        {
            Material[] m = glow.meshRenderer.sharedMaterials;
            m[glow.Value] = matGlow;
            glow.meshRenderer.sharedMaterials = m;
        }
        foreach (GlowStruct glow in hologramObjects)
        {
            Material[] m = glow.meshRenderer.sharedMaterials;
            m[glow.Value] = matHolo;
            glow.meshRenderer.sharedMaterials = m;
        }
    }

    [Button]
    public void SetRandomColour()
    {
        Rareity r = (Rareity)System.Enum.ToObject(typeof(Rareity), Random.Range(0, 3));
        UnityEngine.Debug.Log(r);
        SetType(r);
    }





    [Header("Debug")]
    public bool Debug;
    [Button]
    public void SetGlowObjects()
    {
        glowObjects.Clear();

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            var t = child.GetComponent<MeshRenderer>();
            if (t != null)
            {
                for (int i = 0; i < t.sharedMaterials.Length; i++)
                {
                    if (t.sharedMaterials[i].name.Contains("Glow"))
                    {
                        glowObjects.Add(new GlowStruct
                        {
                            meshRenderer = t,
                            Value = i
                        });
                    }


                }
            }
        }

      
       
    }
    [Button]
    public void SetHologramObjects()
    {
        Transform[] allChildren = Items.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            MeshRenderer mr = child.GetComponent<MeshRenderer>();
            
            if (mr != null)
            {
                for (int i = 0; i < mr.sharedMaterials.Length; i++)
                {
                    hologramObjects.Add(new GlowStruct
                    {
                        meshRenderer = mr,
                        Value = i
                    });
                }
            }
           
        }
    }
}
[System.Serializable]
public struct GlowStruct
{
    public MeshRenderer meshRenderer;
    public int Value;
}
