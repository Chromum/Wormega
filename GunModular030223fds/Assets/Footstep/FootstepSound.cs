using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] landSounds;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float baseFootstepInterval = 0.5f;
    [SerializeField] private float current = 0.5f;

    public FPSCam FPSCam;
    
    private float lastFootstepTime;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (FPSCam.running)
            current = 0.5f / 2f;
        else
            current = baseFootstepInterval;
        
        if ((FPSCam.movementVector.x != 0 || FPSCam.movementVector.z != 0) && FPSCam.movementVector.y <= 0 && !FPSCam.inAir)
        {
            if (Time.time - lastFootstepTime >= current)
            {
                PlayFootstepSound();
                lastFootstepTime = Time.time;
            }
        }
    }

    private void PlayFootstepSound()
    {
        int index = Random.Range(0, footstepSounds.Length);

        AudioUtils.PlaySoundWithPitch(audioSource,footstepSounds[index],Random.Range(.9f,1.1f));
    }

    public void Jump()
    {
        int index = Random.Range(0, jumpSounds.Length);
        AudioUtils.PlaySoundWithPitch(audioSource,jumpSounds[index],Random.Range(.9f,1.1f));

    }

    public void Landed()
    {
        Debug.Log("Landed");
        int index = Random.Range(0, landSounds.Length);
        AudioUtils.PlaySoundWithPitch(audioSource,landSounds[index],Random.Range(.9f,1.1f));

    }
}
