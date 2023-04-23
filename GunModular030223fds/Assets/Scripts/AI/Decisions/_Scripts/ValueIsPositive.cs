using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Decisions/ValueIsPositive")]
public class ValueIsPositive : AIDecision
{
    public override bool Decide(AIBase AIbase)
    {
        Test t = AIbase.GetComponent<Test>();


        if (t.testBool)
            return true;
        else return false;
    }
}
