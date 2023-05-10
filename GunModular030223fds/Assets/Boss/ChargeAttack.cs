using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Wormega/Attacks/ChargeGun")]
public class ChargeAttack : Attack
{
    public float ChargeTime;
    public float Damage;

    public override void Execute(Enemy en)
    {
        en.StartCoroutine(ChargeAttackEE(en));
    }


    public IEnumerator ChargeAttackEE(Enemy en)
    {
        BossAI ai = (BossAI)en;
        //ai.ChargeVFX.SetActive(true);
        yield return new WaitForSeconds(ChargeTime);
        ai.lineRenderer.positionCount = 2;
        ai.lineRenderer.SetPosition(0, ai.ChargeTransform.position);
        ai.lineRenderer.SetPosition(1, (ai.ChargeTransform.transform.position + ai.ChargeTransform.transform.forward * 100f));
        RaycastHit hitInfo;
        if (Physics.SphereCast(ai.ChargeTransform.position, 1f, ai.ChargeTransform.forward,out hitInfo))
        {
            if(hitInfo.collider.CompareTag("Player"))
            {
                Damageable d = hitInfo.collider.GetComponent<Damageable>();
                d.DoDamage(Damage, Vector3.zero);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        ai.lineRenderer.positionCount = 0;
        yield return null;
    }

}
