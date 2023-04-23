using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sam Green/AI/Actions/FacePlayerAction")]
public class FacePlayerAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        Quaternion lookRotation = Quaternion.LookRotation(AIbase.Enemy.player.transform.position - AIbase.transform.position);
        Vector3 rotation = lookRotation.eulerAngles;
        AIbase.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
