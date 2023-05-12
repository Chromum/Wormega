using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    
    public Gun Gun1, Gun2;
    public List<Transform> HealerPositions;
    public AIState CurrentResetState;

    public float WaveOneHealth;
    public float WaveTwoHealth;
    public float WaveThreeHealth;

    public bool HealersSpawned;
    public int currentHealers = 0;

    public Countdown healerSpawnCooldown;

    public int wave = 1;

    public bool hasCharged;
    public float distanceFromPlayerStart;

    public HealthBar Wave1, Wave2, Wave3;


    [Header("Charger Stats")] 
    public float minDamage;
    public float maxDamage;
    public float minKnockbackForce;
    public float maxKnockbackForce;


    public BossRoomManager bossRoomManager;
    public GameObject ChargeVFX;
    public Transform ChargeTransform;
    public LineRenderer lineRenderer;

    public GameObject IntroAnimation;

    public Transform spawnTransform;

    // Start is called before the first frame update

    public void OnEnable()
    {
        spawnTransform.parent.gameObject.GetComponent<Animator>().enabled = false;
        spawnTransform.gameObject.SetActive(false);
        Vector3 pos = transform.position;
        transform.parent = null;
        GameManager.instance.Wave1.transform.parent.gameObject.SetActive(true);
        base.Start();
        healerSpawnCooldown.StartCountdown();
        b.Damageable.Death += WaveChange;
        if(Wave1 == null)
            Wave1 = GameObject.Find("Wave1HealthBar").GetComponent<HealthBar>();
        if(Wave2 == null)
            Wave2 = GameObject.Find("Wave2HealthBar").GetComponent<HealthBar>();
        if(Wave3 == null)
            Wave3 = GameObject.Find("Wave3HealthBar").GetComponent<HealthBar>();

        switch (wave)
        {
            case 2:
                b.Damageable.healthBar = Wave2;
                b.Damageable.MaxHealth = WaveTwoHealth;
                b.Damageable.Health = WaveTwoHealth;
                break;
            case 3:
                b.Damageable.healthBar = Wave3;
                b.Damageable.MaxHealth = WaveThreeHealth;
                b.Damageable.Health = WaveTwoHealth;
                
                break;
            case 4:
                Debug.Log("Boss Defeated");
                Wave1.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
                break;
                
        }

        b.Damageable.healthBar.HealthBarSlider.maxValue = b.Damageable.MaxHealth;
        b.Damageable.healthBar.HealthBarSlider.value = b.Damageable.MaxHealth;
        b.enabled = true;
    }

    public void OnDisable()
    {
        b.Damageable.Death -= WaveChange;

    }

    public void Activate()
    {
        

    }

    public void BackIntoRing()
    {
        ActivateGameOBJ();
        animator.SetBool("InArena", true);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (currentHealers == 0 && HealersSpawned == true)
        {
            HealersSpawned = false;
            healerSpawnCooldown.StartCountdown();
        }

        healerSpawnCooldown.CountdownUpdate();
        BossAttack ba = (BossAttack)Attack;
        foreach (var a in ba.f)
        {
            a.countDown.CountdownUpdate();
        }

        if (Wave1 != null)
        {
            if(wave == 1)
                Wave1.HealthBarSlider.value = b.Damageable.Health;

            if(wave == 2)
                Wave2.HealthBarSlider.value = b.Damageable.Health;

            if(wave == 3)
                Wave3.HealthBarSlider.value = b.Damageable.Health;
        }
        else
        {
            Wave1 = GameManager.instance.Wave1;
            Wave2 = GameManager.instance.Wave2;
            Wave3 = GameManager.instance.Wave3;
        }


    }


    public void HealerDied(GameObject g)
    {
        currentHealers--;
        Destroy(g);
    }

    public bool outOfArena;

    public void WaveChange(GameObject g)
    {
        wave++; 
        switch (wave)
        {
            case 2:
                Wave1.gameObject.SetActive(false);
                Wave2.gameObject.SetActive(true);
                b.Damageable.MaxHealth = WaveTwoHealth;
                b.Damageable.Health = b.Damageable.MaxHealth;
                break;
            case 3:
                Wave2.gameObject.SetActive(false);
                Wave3.gameObject.SetActive(true);
                b.Damageable.MaxHealth = WaveThreeHealth;
                b.Damageable.Health = b.Damageable.MaxHealth;
                break;
            case 4:
                Debug.Log("Boss Defeated");
                Wave1.transform.parent.gameObject.SetActive(false);
                GameManager.instance.GameWin();
                Destroy(gameObject);
                break;
                
        }

        if (wave != 4)
        {
            outOfArena = true;
            SpawnEnemies();
            animator.SetBool("InArena", false);
        }

    }

    public void SpawnEnemies()
    {
        bossRoomManager.SpawnEnemies();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (hasCharged == true)
        {
            if (collision.collider.tag == "Player")
            {
                
                distanceFromPlayerStart = Vector3.Distance(transform.position, player.transform.position);
                float distanceFromPlayerEnd = Vector3.Distance(NavMeshAgent.transform.position, player.transform.position);
                float damageRatio = 1f - (distanceFromPlayerEnd / distanceFromPlayerStart);
                
                float damage = Mathf.Lerp(Stats.DamageMultiplier * minDamage, Stats.DamageMultiplier * maxDamage, damageRatio);
                float knockbackRatio = 1f - damageRatio;
                float knockbackForce = Mathf.Lerp(minKnockbackForce, maxKnockbackForce, knockbackRatio);
                Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                player.GetComponent<Damageable>().DoDamage(damage, player.transform.position);
            }
            hasCharged = false;
            NavMeshAgent.velocity = Vector3.zero;
        }
    }

    public override void ResetCooldown()
    {
        
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void ActivateGameOBJ()
    {
        gameObject.SetActive(true);
    }
}
