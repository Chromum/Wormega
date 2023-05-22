using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public bool SkinnedMeshRender;
    [DisableIf("SkinnedMeshRender")]
    public List<MeshRenderer> mR = new List<MeshRenderer>();
    [EnableIf("SkinnedMeshRender")]
    public List<SkinnedMeshRenderer> sMR = new List<SkinnedMeshRenderer>();
    public Material blackHoleMaterial;
    public Material ExplosionMaterial;
    public Material fireMaterial;
    public Material iceMaterial;
    public Material electricMaterial;
    public bool hasGlow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Button]
    public void SetOutline()
    {
        if (!SkinnedMeshRender)
        {
            foreach (var VARIABLE in mR)
            {
                Material[] materials = VARIABLE.sharedMaterials;
                int existingMaterialsCount = materials.Length;
                bool outlineExists = false;

                for (int i = 0; i < existingMaterialsCount; i++)
                {
                    if (materials[i] == blackHoleMaterial)
                    {
                        outlineExists = true;
                        break;
                    }
                }

                if (!outlineExists)
                {
                    Material[] newArray = new Material[existingMaterialsCount + 1];

                    for (int i = 0; i < existingMaterialsCount; i++)
                    {
                        newArray[i] = materials[i];
                    }

                    newArray[newArray.Length - 1] = blackHoleMaterial;
                    VARIABLE.sharedMaterials = newArray;
                }
            }
        }
        else
        {
            foreach (var VARIABLE in sMR)
            {
                Material[] materials = VARIABLE.sharedMaterials;
                int existingMaterialsCount = materials.Length;
                bool outlineExists = false;

                for (int i = 0; i < existingMaterialsCount; i++)
                {
                    if (materials[i] == blackHoleMaterial)
                    {
                        outlineExists = true;
                        break;
                    }
                }

                if (!outlineExists)
                {
                    Material[] newArray = new Material[existingMaterialsCount + 1];

                    for (int i = 0; i < existingMaterialsCount; i++)
                    {
                        newArray[i] = materials[i];
                    }

                    newArray[newArray.Length - 1] = blackHoleMaterial;
                    VARIABLE.sharedMaterials = newArray;
                }
            }
        }
        

        hasGlow = true;
    }

    [Button]
    public void RemoveOutline()
    {
        
        if (hasGlow)
        {
            if (!SkinnedMeshRender)
            {
                foreach (var VARIABLE in mR)
                {
                    Material[] materials = VARIABLE.sharedMaterials;

                    if (materials.Length > 1)
                    {
                        Material[] newArray = new Material[materials.Length - 1];

                        for (int i = 0; i < newArray.Length; i++)
                        {
                            newArray[i] = materials[i];
                        }

                        VARIABLE.sharedMaterials = newArray;
                    }
                }
            }
            else
            {
                foreach (var VARIABLE in sMR)
                {
                    Material[] materials = VARIABLE.sharedMaterials;

                    if (materials.Length > 1)
                    {
                        Material[] newArray = new Material[materials.Length - 1];

                        for (int i = 0; i < newArray.Length; i++)
                        {
                            newArray[i] = materials[i];
                        }

                        VARIABLE.sharedMaterials = newArray;
                    }
                }
            }
            

            hasGlow = false;
        }
        
    }
    
}
