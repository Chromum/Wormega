using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Attack Attack;
    public EnemyStats EnemyStats;
    public EnemyDifficultyStats DifficultyStats;
    public NavMeshAgent NavMeshAgent;
    public Countdown countdown;
    public float cooldown;
    public bool canAttack;
    public GameObject player;
    public Animator animator;
    public AIBase b;

    public virtual void Start()
    {
        player = GameObject.Find("Player");
        b = gameObject.GetComponent<AIBase>();
        b.enabled = true;
        if(this.GetType() != typeof(BossAI))
            cooldown = EnemyStats.attackCooldown;
        else
        {

            cooldown = Attack.Cooldown;
        }
        countdown.Count = cooldown;
        Debug.Log(GameManager.instance.currentDifficulty.enemyStats.Barrel.Name);
        DifficultyStats = GameManager.instance.currentDifficulty.enemyStats;
        if(this.GetType() != typeof(BossAI))
            b.Damageable.MaxHealth = EnemyStats.Health;
    }
    public virtual void Update()
    {
        if(DifficultyStats.HealthMultiplier != GameManager.instance.currentDifficulty.enemyStats.HealthMultiplier)
            DifficultyStats = GameManager.instance.currentDifficulty.enemyStats;


        if (!canAttack)
        {
            countdown.CountdownUpdate();
            animator.SetBool("AttackAnim", false);
        }

        if (countdown.HasFinished())
        {
            if(canAttack == false)
                canAttack = true;
        }
       
    }

    public virtual void ResetCooldown()
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


    public void ReturnToPool()
    {
        b.Damageable.Health = b.Damageable.MaxHealth;
        b.CurrentState = b.initialState;
    }
}
