using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Gun/Module")]
public class Module : GunPart
{
    public BulletType BulletType = BulletType.NoType;
    public float EffectCooldown;
    public int PassiveDamage;
    public float effectDuration;
    public bool effectOnDeath;
    public GameObject blackHolePrefab;
    



    public void ModuleDeathEvent()
    {
        switch (BulletType)
        {
            case BulletType.Electric:
                break;
            case BulletType.Explosion:
                break;
            case BulletType.Fire:
                break;
            case BulletType.Ice:
                break;
            case BulletType.BlackHole:
                break;
            default:
                break;
        }
    }

    public void ModuleHitEventa()
    {
        switch (BulletType)
        {
            case BulletType.Electric:
                break;
            case BulletType.Explosion:
                break;
            case BulletType.Fire:
                break;
            case BulletType.Ice:
                break;
            case BulletType.BlackHole:
                break;
            default:
                break;
        }
    }
}

public enum BulletType { NoType, Fire, Ice, Electric, Explosion, BlackHole }