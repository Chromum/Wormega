using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeDamageDo : MonoBehaviour
{
    public EnemyStats stats;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Damageable d = other.gameObject.GetComponent<Damageable>();
            print(other.gameObject.name);
            d.DoDamage(stats.minAttackDamage, other.transform.position);
        }
    }
}
