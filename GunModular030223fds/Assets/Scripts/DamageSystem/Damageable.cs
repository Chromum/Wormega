using System;
 using System.Collections;
using System.Collections.Generic;
 using Unity.Mathematics;
 using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Damageable : MonoBehaviour
{
    public bool hasHealthBar;
    [EnableIf("hasHealthBar")]
    public HealthBar healthBar;

    public bool Regen;
    [EnableIf("Regen")]
    public float RegenSpeed;
    [EnableIf("Regen")]
    public Countdown cooldown;

    public float MaxHealth;
    public float Health;


    public GameObject VFXPrefab;

    public delegate void OnDie(GameObject g);
    public OnDie Death;

    public void Start()
    {
        Health = MaxHealth;
    }

    public void Update()
    {
        
        cooldown.CountdownUpdate();
        if(cooldown.Finished)
        {
            if(Health <MaxHealth)
                GiveHealth(Time.deltaTime * RegenSpeed);
        }
        if(healthBar.Damageable.healthBar.HealthBarSlider.maxValue != MaxHealth)
            healthBar.Damageable.healthBar.HealthBarSlider.maxValue = MaxHealth;
    }

    public void DoDamage(float i, Vector3 Pos)
    {
        GameObject.Instantiate(VFXPrefab, Pos, Quaternion.LookRotation((Camera.main.transform.position - transform.position)));

        if ((Health - i) > 0)
            Health -= i;
        else
            Die();

        Debug.Log("Hit:" + gameObject.name + " for " + i + " amount of damage!");

        if (Regen)
        {
            cooldown.StartCountdown();
        }
    }

    public void GiveHealth(float i)
    {
        Health += i;
    }

    public virtual void Die()
    {
        Death?.Invoke(this.gameObject);
    }
}


public enum DamageableType {Flesh, Wood, Cardboard, Steel}
