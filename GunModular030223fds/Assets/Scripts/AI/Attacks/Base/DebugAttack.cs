using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Attacks/DebugAttack")]
public class DebugAttack : Attack
{
    public float Radius;
    public float BaseDam;
    public override void Execute(Enemy en)
    {
        if (en.canAttack)
        {
            Debug.Log("HI");
            Collider[] t = Physics.OverlapSphere(en.transform.position, Radius);

            foreach (Collider item in t)
            {
                if (item.tag == "Player" || item.tag == "Destructable")
                    item.GetComponent<Damageable>().DoDamage(Damage(item.transform.position, en.transform.position),Vector3.zero);
            }
        }
    }


    public float Damage(Vector3 target, Vector3 self)
    {
        float distance = Vector3.Distance(self, target);
        float damage = BaseDam / distance;
        Debug.Log(damage);

        if (damage > BaseDam)
            return BaseDam;
        else return damage;
    }
}
