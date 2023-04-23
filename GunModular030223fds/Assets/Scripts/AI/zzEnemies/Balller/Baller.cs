using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Baller : Enemy
{
    [Header("Tubes")] public Tube Tube1;
    public Tube Tube2;
    public Tube Tube3;
    public Tube Tube4;
    public Tube Tube5;
    [Space(7.5f)]
    [Header("Sounds")]
    public AudioClip[] MissleShoot;
    public AudioClip[] Explosion;
    
    // Start is called before the first frame update


}

[System.Serializable]
public struct Tube
{
    public VisualEffect VFX;
    public AudioSource As;

}
