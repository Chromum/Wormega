 using System;
 using System.Collections;
using System.Collections.Generic;
 using Unity.Mathematics;
 using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float MaxHealth;
    public float Health;


    public GameObject VFXPrefab;

    public void Start()
    {
        Health = MaxHealth;
    }

    public void DoDamage(float i, Vector3 Pos)
    {
        GameObject.Instantiate(VFXPrefab, Pos, Quaternion.LookRotation((Camera.main.transform.position - transform.position)));
        
        if ((Health - i) > 0)
            Health -= i;
        else
            Die();

        Debug.Log("Hit:" + gameObject.name);
        
        
    }

    public void GiveHealth(float i)
    {
        Health += i;
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}


public enum DamageableType {Flesh, Wood, Cardboard, Steel}
