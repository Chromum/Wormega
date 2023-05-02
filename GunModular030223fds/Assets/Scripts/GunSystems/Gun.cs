using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public bool AI;
    public BaseStat BaseStat;
    public AudioSource sU;
    public AudioClip clip;
    public AudioClip Click;
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


    public Transform fireTar;
    public TextMeshProUGUI AmmoCount;

    public GunRecoil gunRecoil;


    public Countdown Countdown;
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
                Shoot(Dir);
            else GunError(0);
        }
    }

    public void GunError(int i)
    {
        switch (i)
        {
            case 0:
                AudioUtils.PlaySoundWithPitch(sU, Click, 1f);
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

        FireRate = fireRate;
        this.Damage = Damage;
        MagSize = magSize;
        this.Accuracy = Accuracy;
        Reload();
    }

    public void Reload()
    {
        currentAmmo = MagSize;
        //AudioUtils.PlaySoundWithPitch(sU, Click, 1f);
    }

    public void Shoot(Vector3 Direction)
    {
        Countdown.Count = MathsUtils.DecreaseFloatByPercentage(BaseStat.FireRateBase, FireRate);
        Countdown.StartCountdown();
        fireTar.localEulerAngles = new Vector3(0f, 0f, 0f);
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
        currentAmmo -= count;
        float Accuracy = 0f;
        if (AI)
        {
            Accuracy = 0.03f;
        }
        else Accuracy = this.Accuracy;

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
        if (AI)
            ShotCount = 1;

        for (int i = 0; i < ShotCount; i++)
        {
            if (ShotCount == 1)
            {
                RaycastHit h1;

                if (RaycastWithAccuracy(fireTar.position, Direction, out h1, Mathf.Infinity, Accuracy) )
                {
                    Damageable d = h1.collider.transform.GetComponent<Damageable>();
                    if (d != null)
                    {
                        d.DoDamage((float)Damage,h1.point);
                    }

                }
                GunShot();
                
            }
            if (ShotCount == 2)
            {
                RaycastHit h1;
                RaycastHit h2;
                switch (i)
                {
                    case 0:
                        fireTar.localEulerAngles = new Vector3(0f, -5f, 0f);
                        if (RaycastWithAccuracy(fireTar.position, Direction, out h1, Mathf.Infinity, Accuracy))
                        {
                            Damageable d = h1.collider.transform.GetComponent<Damageable>();
                            if (d != null)
                            {
                                d.DoDamage((float)Damage,h1.point);
                            }
                        }
                        GunShot();
                        Debug.Log("Shot");
                        break;
                    case 1:
                        fireTar.localEulerAngles = new Vector3(0f, 5f, 0f);
                        if (RaycastWithAccuracy(fireTar.position, Direction, out h2, Mathf.Infinity, Accuracy))
                        {
                            Damageable d = h2.collider.transform.GetComponent<Damageable>();
                            if (d != null)
                            {
                                d.DoDamage((float)Damage,h2.point);
                            }
                        }
                        GunShot();
                        Debug.Log("Shot");
                        break;

                }
                
            }
            if (ShotCount == 3)
            {
                RaycastHit h1;
                RaycastHit h2;
                RaycastHit h3;
                
                switch (i)
                {
                    case 0:
                        fireTar.localEulerAngles = new Vector3(0f, -10f, 0f);
                        if (RaycastWithAccuracy(fireTar.position, Direction, out h1, Mathf.Infinity,Accuracy))
                        {
                            Damageable d = h1.collider.transform.GetComponent<Damageable>();
                            if (d != null)
                            {
                                d.DoDamage((float)Damage,h1.point);
                            }
                        }
                        GunShot();
                        Debug.Log("Shot");
                        break;
                    case 1:
                        fireTar.localEulerAngles = new Vector3(0f, 0f, 0f);
                        if (RaycastWithAccuracy(fireTar.position, Direction, out h2, Mathf.Infinity, Accuracy))
                        {
                            Damageable d = h2.collider.transform.GetComponent<Damageable>();
                            if (d != null)
                            {
                                d.DoDamage((float)Damage,h2.point);
                            }
                        }
                        GunShot();
                        Debug.Log("Shot");
                        break;
                    case 2:
                        fireTar.localEulerAngles = new Vector3(0f, 10f, 0f);
                        if (RaycastWithAccuracy(fireTar.position, Direction, out h3, Mathf.Infinity, Accuracy))
                        {
                            Damageable d = h3.collider.transform.GetComponent<Damageable>();
                            if (d != null)
                            {
                                d.DoDamage((float)Damage,h3.point);
                            }
                        }

                        GunShot();
                        Debug.Log("Shot");
                        break;
                }
            }
        }

        gunRecoil.Fire();
    }
    public bool RaycastWithAccuracy(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, float accuracy)
    {
        Vector3 spreadDirection = Vector3.Slerp(direction, UnityEngine.Random.onUnitSphere, accuracy);

        if (Physics.Raycast(origin, spreadDirection, out hitInfo, maxDistance))
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
        AudioUtils.PlaySoundWithPitch(sU,clip,pitch);
        GameObject g = GameObject.Instantiate(VFX,transform.GetChild(0).transform.position,new Quaternion(0f,-180f,0f,0f), transform.GetChild(0));
        g.transform.localPosition = new Vector3(0f, 0f, -0.366f);
        g.transform.localRotation = new Quaternion(0f, -180f, 0f, 0f);
    }
}

public enum GunPartEnum{ Mag, Grip, Sight }


