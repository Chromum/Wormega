using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ItemBox : MonoBehaviour
{
    public List<GlowStruct> glowObjects = new List<GlowStruct>();
    public List<GlowStruct> hologramObjects = new List<GlowStruct>();

    [Button]
    public void OpenBox()
    {

    }








    [Header("Debug")]
    public bool Debug;
    [Button]
    public void SetGlowObjects()
    {

    }
    [Button]
    public void SetHologramObjects()
    {

    }
}

public struct GlowStruct
{
    public MeshRenderer meshRenderer;
    public int Value;
}
