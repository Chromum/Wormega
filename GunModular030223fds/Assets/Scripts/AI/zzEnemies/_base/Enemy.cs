using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Attack Attack;
    public EnemyDifficultyStats Stats;
    public NavMeshAgent NavMeshAgent;
    public Countdown countdown;
    public float cooldown;
    public bool canAttack;
    public GameObject player;
    public Animator animator;
    public AIBase b;

    public void Start()
    {
        player = GameObject.Find("Player");
        b = gameObject.GetComponent<AIBase>();
        b.enabled = true;
        cooldown = Attack.Cooldown;
        countdown.Count = cooldown;
        Stats = GameManager.instance.currentDifficulty.enemyStats;
    }


    public void Update()
    {
        if (!canAttack)
        {
            countdown.CountdownUpdate();
        }

        if (countdown.HasFinished())
        {
            if(canAttack == false)
                canAttack = true;
            

        }
       
    }

    public void ResetCooldown()
    {
        canAttack = false;
        countdown.StartCountdown();
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
