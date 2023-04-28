using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornedEnemy : Enemy
{
    public AudioSource AU;
    public AudioClip Charge, Shot;
    public bool hasCharged;
    public float distanceFromPlayerStart;
    public void OnCollisionEnter(Collision collision)
    {
        if (hasCharged == true)
        {
            if (collision.collider.tag == "Player")
            {
                ChargerAttack a = Attack as ChargerAttack;
                distanceFromPlayerStart = Vector3.Distance(transform.position, player.transform.position);
                float distanceFromPlayerEnd = Vector3.Distance(NavMeshAgent.transform.position, player.transform.position);
                float damageRatio = 1f - (distanceFromPlayerEnd / distanceFromPlayerStart);
                float damage = Mathf.Lerp(a.minDamage, a.maxDamage, damageRatio);

                float knockbackRatio = 1f - damageRatio;
                float knockbackForce = Mathf.Lerp(a.minKnockbackForce, a.maxKnockbackForce, knockbackRatio);
                Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                player.GetComponent<Damageable>().DoDamage(damage, player.transform.position);
            }
            hasCharged = false;
            NavMeshAgent.velocity = Vector3.zero;
        }
    }


    public void ChargeUp()
    {
        AudioUtils.PlaySoundWithPitch(AU, Charge, 1f);
    }
    public void StopSound()
    {
        AU.Stop();
    }
    public void ChargeRelease()
    {
        AudioUtils.PlaySoundWithPitch(AU, Shot, 1f);
    }
}