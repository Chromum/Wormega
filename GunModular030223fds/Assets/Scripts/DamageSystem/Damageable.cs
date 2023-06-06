using System;
 using System.Collections;
using System.Collections.Generic;
 using Unity.Mathematics;
 using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class Damageable : MonoBehaviour
{
    public bool isEnemy;
    public bool isBoss;
    public bool hasHealthBar;
    [EnableIf("hasHealthBar")]
    public HealthBar healthBar;
    [EnableIf("hasHealthBar")]
    public Countdown cooldownBar;


    public bool Regen;
    [EnableIf("Regen")]
    public float RegenSpeed;
    [EnableIf("Regen")]
    public Countdown cooldown;

    [EnableIf("isEnemy")] public Transform hitTextPos;

    public bool Player;
    [EnableIf("Player")]
    public TextMeshPro text;
    public float HealthTextValue;

    public float MaxHealth;
    public float Health;


    public GameObject VFXPrefab;

    public delegate void OnDie(GameObject g);
    public OnDie Death;
    public delegate void OnHit();
    public OnHit Hit;

    private AudioSource Au;

    public float startingHealth;
    public Poolee HitFX;
    public Poolee hitText;
    public PooleeManager poolManager;
    public void Start()
    {
        Health = MaxHealth;
        startingHealth = MaxHealth;
        HealthTextValue = 0f;
        Au = GetComponent<AudioSource>();
    }



    public void Update()
    {
        
        cooldown.CountdownUpdate();
        if(cooldown.Finished)
        {
            if(Health <MaxHealth)
                GiveHealth(Time.deltaTime * RegenSpeed);
        }

        if (healthBar)
        {
            if (healthBar.HealthBarSlider.maxValue != MaxHealth)
                healthBar.HealthBarSlider.maxValue = MaxHealth;
        }
        
        if (!isEnemy)
        {
            string s = Mathf.Round((Health / (startingHealth + (HealthTextValue))) * (100 + (HealthTextValue))).ToString();
            string[] split = s.Split(":");
            
            text.text = split[0].ToString() + "%";
        }



        if (!isBoss)
        {
            if (hasHealthBar && isEnemy)
            {
                cooldownBar.CountdownUpdate();
                if (cooldownBar.HasFinished())
                {
                    healthBar.HealthBarSlider.gameObject.SetActive(false);
                }



            }
        }
        
        
    }

    public void DoDamage(float i, Vector3 Pos)
    {

        if ((Health - i) > 0)
            Health -= i;
        else
            Die();

        Debug.Log("Hit:" + gameObject.name + " for " + i + " amount of damage!");

        if (Regen)
        {
            cooldown.StartCountdown();
        }

        if(isEnemy)
        {
            SpawnHitText(i);
            AudioUtils.PlaySoundWithPitch(GameManager.instance.AudioSource, GameManager.instance.HitMarker, 1f);
        }

        if (hasHealthBar)
        {
            cooldownBar.StartCountdown();
            
            if(!isBoss)
                if(healthBar.HealthBarSlider.gameObject.activeSelf == false)
                    healthBar.HealthBarSlider.gameObject.SetActive(true);

            

        }
        
        Hit?.Invoke();
    }

    public void GiveHealth(float i)
    {
        Health += i;
    }

    public virtual void Die()
    {
        Death?.Invoke(this.gameObject);

        
        
        if (isEnemy)
        {
            GameAnnouncer a = GameObject.FindObjectOfType<GameAnnouncer>();
            if(poolManager != null)
                PoolManager.instance.ReturnToPool(poolManager.poolType,gameObject);
            a.KillTracker.EnemyDied(a.Announcer);
            
        }
        if (gameObject.tag == "Player")
        {
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
    }
    public void DeathEvent()
    {
        Cursor.visible = true;
        GameManager.instance.PlayerDeath();
    }

    public void SpawnHitText(float Damage)
    {
        string[] s = Damage.ToString().Split(".");
        string text = s[0];

        GameObject g = PoolManager.instance.SpawnFromPool(hitText, hitTextPos.position, Quaternion.identity);
        g.GetComponent<HitText>().TextMeshPro.text = text;


    }
    
    
    

    
}


public enum DamageableType {Flesh, Wood, Cardboard, Steel}
