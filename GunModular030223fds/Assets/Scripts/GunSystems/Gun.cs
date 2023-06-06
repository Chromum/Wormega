     using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
 using Unity.Mathematics;
 using UnityEngine;
using UnityEngine.VFX;
 using Random = UnityEngine.Random;

 public class Gun : MonoBehaviour
{
    public bool AI;
    public bool boss;
    public float bossAccuracy;
    public BaseStat BaseStat;
    public AudioSource sU;
    public List<AudioClip> clip;
    public List<AudioClip> Click;
    public GameObject VFX;
    
    [Header("Stats")]   
    public float FireRate;
    public float Damage;
    public int MagSize;
    public float Accuracy;

    [Header("Parts")]
    public GunPart Magazine;
    public GunPart Grip;
    public GunPart Sight;
    public Barrel Barrel;
    public Module Module;

    public int currentAmmo;
    public LayerMask mask;

    public Transform fireTar;
    public TextMeshPro AmmoCount;

    public GunRecoil gunRecoil;

    public List<Poolee> decals;
    
    public Countdown Countdown;

    public Countdown bulletTypeCooldown;
    public void Start()
    {

        RecalculateStats();
        Countdown.Count = MathsUtils.DecreaseFloatByPercentage(BaseStat.FireRateBase, FireRate);
    }

    public void Update()
    {

        Countdown.CountdownUpdate();

        if(!AI)
        {
            if (AmmoCount.text != currentAmmo + "/" + MagSize)
            {
                AmmoCount.text = currentAmmo + "/" + MagSize;
            }
        }

    }

    public bool NeedsToReload()
    {
        if (AI )
            return false;
        
        
        int count = 0;
        switch (Barrel.ShotMode)
        {
            case ShotMode.SingleShot:
                count = 1;
                break;
            case ShotMode.DoubleShot:
                count = 2;
                break;
            case ShotMode.TrippleShot:
                count = 3;
                break;
            default:
                break;
        }

        if (currentAmmo >= count)
            return false;
        else return true;
    }

    public void TryShoot(Vector3 Dir)
    {
        if (Countdown.HasFinished())
        {
            if (NeedsToReload() == false)
                StartCoroutine(Shoot(Dir));
            else
            {
                Countdown.StartCountdown();
                GunError(0);
                Reload();
            }
        }
    }

    public void GunError(int i)
    {
        switch (i)
        {
            case 0:
                AudioUtils.PlaySoundWithPitch(sU, Click[Random.RandomRange(0,Click.Count)], 1f);
                break;
            default:
                break;
        }
    }    

    public void UpdateGunPart(GunPartEnum e, GunPart g)
    {
        switch (e)
        {
            case GunPartEnum.Grip:
                Grip = g;
                break;
            case GunPartEnum.Mag:
                Magazine = g;
                break;
            case GunPartEnum.Sight:
                Sight = g;
                break;
        }

        RecalculateStats(); 
    }
    [NaughtyAttributes.Button("Recalculate Stats")]
    public void RecalculateStats()
    {

        float fireRate = BaseStat.FireRateBase;
        float Damage = BaseStat.DamageBase;
        int magSize = BaseStat.MagSizeBase;
        float Accuracy = BaseStat.AccuracyBase;


        fireRate += Magazine.FireRate;
        fireRate += Grip.FireRate;
        fireRate += Sight.FireRate;
        fireRate += Barrel.FireRate;
        fireRate += Module.FireRate;
        
        Damage += Magazine.Damage;
        Damage += Grip.Damage;
        Damage += Sight.Damage;
        Damage += Barrel.Damage;
        Damage += Module.Damage;
        
        
        magSize += Magazine.MagSize;
        magSize += Grip.MagSize;
        magSize += Sight.MagSize;
        magSize += Barrel.MagSize;
        magSize += Module.MagSize;
        
        
        Accuracy += Magazine.Accuracy;
        Accuracy += Grip.Accuracy;
        Accuracy += Sight.Accuracy;
        Accuracy += Barrel.Accuracy;
        Accuracy += Module.Accuracy;

        if (boss)
            Accuracy = bossAccuracy;

        FireRate = fireRate;
        this.Damage = Damage;
        MagSize = magSize;
        this.Accuracy = Accuracy;

        if (Accuracy < 0)
            Accuracy = 0;
        
    }

    public void Reload()
    {
        currentAmmo = MagSize;
        //AudioUtils.PlaySoundWithPitch(sU, Click, 1f);
    }

    public IEnumerator Shoot(Vector3 Direction)
    {
        Countdown.Count = MathsUtils.DecreaseFloatByPercentage(BaseStat.FireRateBase, FireRate);
        Countdown.StartCountdown();
        fireTar.localEulerAngles = new Vector3(0f, 0f, 0f);
        int ShotCount = 1;
        switch (Barrel.ShotMode)
        {
            case ShotMode.SingleShot:
                ShotCount = 1;
                break;
            case ShotMode.DoubleShot:
                ShotCount = 2;
                break;
            case ShotMode.TrippleShot:
                ShotCount = 3;
                break;
            default:
                break;
        }

        if (AI && !boss)
        {
            ShotCount = 1;
            Accuracy = .1f;
        }

        
        for (int i = 0; i < ShotCount; i++)
        {
            RaycastHit h1;
            Debug.Log("BANG");
            if (RaycastWithAccuracy(fireTar.position, Direction, out h1, Mathf.Infinity, Accuracy) )
            {
                Damageable d = h1.collider.transform.GetComponent<Damageable>();
                if (d != null)
                {
                    d.DoDamage((float)Damage,h1.point);
                }
                else
                {
                    SpawnDecal(h1.point,h1.normal);
                }

            }
            GunShot();
            currentAmmo--;
            yield return new WaitForSeconds(.01f);
        }
       
    }

    public void SpawnDecal(Vector3 position, Vector3 rotation)
    {
        PoolManager.instance.SpawnFromPool(decals[Random.RandomRange(0, decals.Count)], position, Quaternion.FromToRotation(Vector3.forward, rotation));
    }
    
    public bool RaycastWithAccuracy(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, float accuracy)
    {
        Vector3 spreadDirection = Vector3.Slerp(direction, UnityEngine.Random.onUnitSphere, accuracy);

        if (Physics.Raycast(origin, spreadDirection, out hitInfo, 1000f,mask))
        {
            return true;
        }

        return false;
    }

    public void OnDrawGizmos()
    {
        

    }
   

    public void GunShot()
    {
        float pitch = UnityEngine.Random.Range(0.9f, 1.1f); // Change pitch randomly within a small range
        AudioUtils.PlaySoundWithPitch(sU,clip[Random.RandomRange(0,clip.Count)],pitch);
        if(!AI)
            gunRecoil.Fire();
        VFX.GetComponent<VisualEffect>().Play();

    }
}

public enum GunPartEnum{ Mag, Grip, Sight }


