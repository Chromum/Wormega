using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkVoiceController : MonoBehaviour
{
    public List<AudioClip> GameStartClips = new List<AudioClip>();
    public List<AudioClip> DeathClips = new List<AudioClip>();
    public List<AudioClip> Grunts = new List<AudioClip>();
    public ItemPickupsEffects ItemPickUpSounds;

    public AudioSource AudioSource;
    public Damageable Damageable;

    public void Start()
    {
        Damageable.Death += PlayerDeathClip;
        Damageable.Hit += PlayerHitClip;
    }

    public void StartGame()
    {
        AudioSource.PlayOneShot(GameStartClips[Random.Range(0,GameStartClips.Count)]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemPickUpPlay(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    public void PlayerDeathClip(GameObject g)
    {
        AudioSource.PlayOneShot(DeathClips[Random.Range(0,DeathClips.Count)]);

    }

    public void PlayerHitClip()
    {
        AudioSource.PlayOneShot(Grunts[Random.Range(0,Grunts.Count)]);
    }
}
[System.Serializable]
public struct ItemPickupsEffects
{
    public List<AudioClip> Common;
    public List<AudioClip> Rare;
    public List<AudioClip> Legendary;
    public List<AudioClip> Mythic;
    
    public List<AudioClip> AllRounders;

}
