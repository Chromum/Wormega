using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wormega/Attacks/GruntAttack")]
public class GruntAttack : Attack
{
    public AIState FollowState;
    public AIDecision clearShot;
    public override void Execute(Enemy en)
    {
        Debug.Log("GruntBang!");
        if (en.GetType() == typeof(Grunt))
        {
            Grunt g = (Grunt)en;
            if (clearShot.Decide(en.b))
            {

                g.AIGun.StartCoroutine(g.AIGun.Shoot(g.player.transform.position - g.AIGun.fireTar.position));
                ShowRayBetweenPoints(g.AIGun.fireTar.position, g.player.transform.GetComponent<CapsuleCollider>().center);
                en.b.CurrentState = FollowState;
            }
        }
        else
        {
            BossAI g = (BossAI)en;
            int i = Random.Range(0, 1);
            if (clearShot.Decide(en.b))
            {
                Debug.Log("I");
                if(i == 0)
                    g.Gun1.StartCoroutine(g.Gun1.Shoot(g.player.transform.position - g.Gun1.fireTar.position));
                else
                    g.Gun2.StartCoroutine(g.Gun1.Shoot(g.player.transform.position - g.Gun2.fireTar.position));

                //ShowRayBetweenPoints(g.AIGun.fireTar.position, g.player.transform.GetComponent<CapsuleCollider>().center);
                en.b.CurrentState = g.CurrentResetState;
            }
        }
       
    }

    

    void ShowRayBetweenPoints(Vector3 p1, Vector3 p2)
    {
        Debug.DrawRay(p1, (p2 - p1), Color.yellow);
    }
}
