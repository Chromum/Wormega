using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Attack Attack;
    public NavMeshAgent NavMeshAgent;
    public float cooldownTimer;
    public bool canAttack;
    public GameObject player;
    public Animator animator;
    public AIBase b;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        b = gameObject.GetComponent<AIBase>();
        cooldownTimer = Attack.Cooldown;
    }


    public void Update()
    {
        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime; 
            if (cooldownTimer <= 0) 
            {
                canAttack = true;
                Debug.Log("Timer Reset");
            }
        }
        
       
    }

    public void ResetCooldown()
    {
        canAttack = false;
        cooldownTimer = Attack.Cooldown;
        Debug.Log("Reseting Timer");
    }

    public void DoAttack()
    {
        if (canAttack)
        {
            ResetCooldown();
            Attack.Execute(this);
            animator.SetBool("AttackAnim", false);

        }
    }
}
