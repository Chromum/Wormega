using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public Transform dir;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 30f; // The speed of the camera rotation
    public float minAngle = 0f; // The minimum angle the camera rotates to
    public float maxAngle = 180f; // The maximum angle the camera rotates to
}
