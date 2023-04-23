using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public float swayMagnitude = 0.1f;
    public float swaySpeed = 0.5f;
    private Vector3 initialPosition;

    public bool Sway = true;


    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (Sway)
        {
            float xSway = Mathf.Sin(Time.time * swaySpeed) * swayMagnitude;
            float ySway = Mathf.Cos(Time.time * swaySpeed) * swayMagnitude;
            transform.localPosition = initialPosition + new Vector3(xSway, ySway, 0);
        }
    }
}
