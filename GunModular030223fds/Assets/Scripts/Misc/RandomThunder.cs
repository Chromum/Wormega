using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThunder : MonoBehaviour
{
    public AudioClip[] thunderSounds;
    public AudioSource audioSource;
    public float delay = 10f; 
    private float timeToNextThunder = 0f;
    public GameObject lightningEffect;
    public float lightningDuration = 0.1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lightningEffect.SetActive(false);
    }

    void Update()
    {
        if (!audioSource.isPlaying && Time.time >= timeToNextThunder)
        {
            int randomIndex = Random.Range(0, thunderSounds.Length);
            audioSource.clip = thunderSounds[randomIndex];

            lightningEffect.SetActive(true);

            Invoke("PlayThunderSound", lightningDuration);

            timeToNextThunder = Time.time + delay + Random.Range(-1f, 1f) * delay * 0.1f;
        }
    }

    void PlayThunderSound()
    {
        audioSource.Play();
        Invoke("TurnOffLightning", .25f);
    }

    void TurnOffLightning()
    {
        lightningEffect.SetActive(false);
    }

}
