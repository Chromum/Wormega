using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/TargetNotNull")]
public class TargetIsNotNull : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        Test t = AIbase.GetComponent<Test>();

        Debug.Log(t.testBool);

        if (t.target != null)
            return true;
        else return false;
    }
}
