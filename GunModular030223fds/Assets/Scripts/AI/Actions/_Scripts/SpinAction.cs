using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Sam Green/AI/Actions/SpinAction")]
public class SpinAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        AIbase.transform.eulerAngles = new Vector3(AIbase.transform.eulerAngles.x + 10f, AIbase.transform.eulerAngles.y + 10f);
    }
}
