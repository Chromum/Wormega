using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Attacks/TurretAttack")]
public class TurretAttack : Attack
{
    public float maxDamage = 50f;
    public float minDamage = 10f;

    public override void Execute(Enemy en)
    {
        en.player.GetComponent<Damageable>().DoDamage(Damage((Turret)en),Vector3.zero);
        en.canAttack = false;
    }


    public float Damage(Turret en)
    {
        float distance = Vector3.Distance(en.dir.transform.position, en.player.transform.position);

        float damage = Mathf.Lerp(maxDamage, minDamage, Mathf.InverseLerp(0f, maxDamage, distance));

        Debug.Log("Distance: " + distance + " Damage: " + damage);

        return damage;
    }
}
