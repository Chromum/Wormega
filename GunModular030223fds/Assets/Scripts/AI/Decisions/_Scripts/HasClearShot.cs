using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/WillHit")]
public class HasClearShot : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        Debug.Log("1");
        RaycastHit hit;
        if(Physics.Raycast(AIbase.transform.position , AIbase.transform.forward, out hit, 10f))
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
