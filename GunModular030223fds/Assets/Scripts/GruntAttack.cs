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
        Grunt g = (Grunt)en;
        if (clearShot.Decide(en.b))
        {

            g.AIGun.Shoot(g.player.transform.position - g.AIGun.fireTar.position);
            ShowRayBetweenPoints(g.AIGun.fireTar.position, g.player.transform.GetComponent<CapsuleCollider>().center);
            en.b.CurrentState = FollowState;
        }
    }

    

    void ShowRayBetweenPoints(Vector3 p1, Vector3 p2)
    {
        Debug.DrawRay(p1, (p2 - p1), Color.yellow);
    }
}
