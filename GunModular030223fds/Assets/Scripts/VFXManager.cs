using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class VFXManager : MonoBehaviour
{
    private VisualEffect vfx;
    private AudioSource audioSource;
    private bool isPlaying;

    void Start()
    {
        // Get the VFX and AudioSource components attached to this GameObject (if any)
        vfx = GetComponent<VisualEffect>();
        audioSource = GetComponent<AudioSource>();

        // Play the VFX and audio if they exist
        if (vfx != null)
        {
            vfx.Play();
            isPlaying = true;
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // Check if both the VFX and audio have finished playing
        if (isPlaying && vfx != null && !vfx.HasAnySystemAwake() && audioSource != null && !audioSource.isPlaying)
        {
            // Destroy the GameObject
            Destroy(gameObject);
        }
        else if (isPlaying && vfx != null && !vfx.HasAnySystemAwake() && audioSource == null)
        {
            Destroy(gameObject);

        }
    }
}
