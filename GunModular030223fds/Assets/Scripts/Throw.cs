using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public MathsUtils.eThrowTypes ThrowType;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool athrow = false;
        if (Input.GetKeyDown(KeyCode.T))
        {
            athrow = true;
            ThrowType = MathsUtils.eThrowTypes.Height;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            athrow = true;
            ThrowType = MathsUtils.eThrowTypes.HighSpeed;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            athrow = true;
            ThrowType = MathsUtils.eThrowTypes.LowSpeed;
        }

        if (athrow)
        {
            athrow = false;
            Debug.Log("Trying to throw");
            Vector3 ThrowVel;
            float FlightTime;
            if (MathsUtils.CalculateTrajectory(rb.position, Target.position, Speed, ThrowType, out ThrowVel,
                    out FlightTime))
            {
                Debug.Log("Throw Will hit");
                rb.velocity = Vector3.zero;
                rb.AddForce(ThrowVel,ForceMode.VelocityChange);
            }
            else
            {
                Debug.Log("Throw Will not hit");
            }
        }
    }
}
