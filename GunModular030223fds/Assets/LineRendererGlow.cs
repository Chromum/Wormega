using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererGlow : MonoBehaviour
{
    public Color glowColor;
    public float minGlowIntensity = 0.5f;
    public float maxGlowIntensity = 1f;
    public float glowIntensity = 1f;

    public Material[] lineMaterials;
    private Color[] originalColors;
    private float currentIntensity;
    private void Start()
    {
        // Store the original emission colors
        originalColors = new Color[lineMaterials.Length];
        for (int i = 0; i < lineMaterials.Length; i++)
        {
            originalColors[i] = lineMaterials[i].GetColor("_EmissionColor");
        }
        currentIntensity = Random.Range(minGlowIntensity, maxGlowIntensity);
    }

    private void Update()
    {
        // Calculate the glowing color based on the glow color and current intensity
        Color newColor = glowColor * currentIntensity;

        // Apply the new color to the materials' emission colors
        for (int i = 0; i < lineMaterials.Length; i++)
        {
            lineMaterials[i].SetColor("_EmissionColor", newColor);
        }

        // Update the current intensity with random pulsation
        float pulsation = Mathf.PingPong(Time.time * glowIntensity, maxGlowIntensity - minGlowIntensity);
        currentIntensity = minGlowIntensity + pulsation;
    }

    private void OnDestroy()
    {
        // Reset the materials' emission colors to the original colors when the script is destroyed
        for (int i = 0; i < lineMaterials.Length; i++)
        {
            lineMaterials[i].SetColor("_EmissionColor", originalColors[i]);
        }
    }

    private Material[] CreateMaterialInstances(Material[] materials)
    {
        Material[] instances = new Material[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            instances[i] = new Material(materials[i]);
        }
        return instances;
    }
}
