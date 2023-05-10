using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class VFXManager : MonoBehaviour
{
    private VisualEffect vfx;
    private AudioSource audioSource;
    private bool isPlaying;
    public Poolee Pool;
    public float minPitch = 1f;
    public float maxPitch = 1f;
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
             AudioUtils.PlaySoundWithPitch(audioSource, audioSource.clip,Random.Range(minPitch,maxPitch));
        }
    }

   

    void Update()
    {
        // Check if both the VFX and audio have finished playing
        if (isPlaying && vfx != null && !vfx.HasAnySystemAwake())
        {
            if(Pool != null) PoolManager.instance.ReturnToPool(Pool,gameObject);
        }

    }
}
