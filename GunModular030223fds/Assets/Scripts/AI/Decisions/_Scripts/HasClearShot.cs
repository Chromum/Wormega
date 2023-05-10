using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/WillHit")]
public class HasClearShot : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        RaycastHit hit;
        if(Physics.Raycast(AIbase.transform.position , AIbase.transform.forward, out hit, 30f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
