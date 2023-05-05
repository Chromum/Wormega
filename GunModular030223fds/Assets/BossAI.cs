using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : Enemy
{
    
    public Gun Gun1, Gun2;
    public List<Transform> HealerPositions;
    public AIState CurrentResetState;

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


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        healerSpawnCooldown.StartCountdown();
        b.Damageable.Death += WaveChange;
        Wave1 = GameObject.Find("Wave1HealthBar").GetComponent<HealthBar>();
        Wave2 = GameObject.Find("Wave2HealthBar").GetComponent<HealthBar>();
        Wave3 = GameObject.Find("Wave3HealthBar").GetComponent<HealthBar>();

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

    public void WaveChange(GameObject g)
    {
        wave++;
        switch (wave)
        {
            case 2:
                Wave1.gameObject.SetActive(false);
                Wave2.gameObject.SetActive(true);
                b.Damageable.Health = b.Damageable.MaxHealth;
                break;
            case 3:
                Wave2.gameObject.SetActive(false);
                Wave2.gameObject.SetActive(true);
                b.Damageable.Health = b.Damageable.MaxHealth;
                break;
            case 4:
                Debug.Log("Boss Defeated");
                Destroy(gameObject);
                break;
                
        }
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
}
