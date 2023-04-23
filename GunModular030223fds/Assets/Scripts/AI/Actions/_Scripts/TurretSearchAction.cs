using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sam Green/AI/Actions/TurretSearchAction")]
public class TurretSearchAction : AIAction
{
    public override void Execute(AIBase AIbase)
    {
        Turret t = (Turret)AIbase.Enemy;
        // Calculate the angle to rotate by
        float angle = Mathf.Sin(Time.time * t.rotationSpeed) * (t.maxAngle - t.minAngle) * 0.5f;

        // Calculate the target rotation based on the current angle and the min/max angles
        float targetRotation = angle + (t.minAngle + t.maxAngle) * 0.5f;

        // Store the current z-axis rotation
        float currentZRotation = AIbase.transform.rotation.eulerAngles.z;

        // Rotate the object around the y-axis by the target rotation
        AIbase.transform.rotation = Quaternion.Euler(0f, targetRotation, currentZRotation);
    }
}
