using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : Interactable
{
    public float Speed;
    public MapSpriteSelector ConnectorOne, ConnectorTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(GameObject GO)
    {
        // var rb = GO.GetComponent<Rigidbody>();
        //
        // var c = rb.GetComponent<FPSCam>();
        // c.contactMod = false;
        //
        // float time;
        // Vector3 Velocity;
        // TryThrow(rb.position, transform.position, Speed, MathsUtils.eThrowTypes.HighSpeed, rb, out time, out Velocity);
        // var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // g.transform.position = transform.position;
        StartCoroutine(Grapple(GO));

    }

    public IEnumerator Grapple(GameObject GO)
    {
        float Speed = this.Speed;
        Rigidbody rb = GO.GetComponent<Rigidbody>();

        FPSCam c = rb.GetComponent<FPSCam>();
        c.contactMod = false;
        
        float time;
        Vector3 Velocity;
        
        Vector3 target;
        if (ConnectorOne.PlayerInRoom)
            target = ConnectorTwo.transform.position;
        else
        {
            target = ConnectorOne.transform.position;
        }
        
        rb.velocity = Vector3.zero;
        TryThrow(rb.position,transform.position,Speed,MathsUtils.eThrowTypes.HighSpeed,rb,out time,out Velocity);
        yield return new WaitForSeconds(time);

        rb.velocity = Vector3.zero;
        TryThrow(rb.position,target,Speed,MathsUtils.eThrowTypes.HighSpeed,rb,out time,out Velocity);

        yield return new WaitForSeconds(time);    
        c.contactMod = true;
    }

    public void TryThrow(Vector3 Pos, Vector3 Target, float Speed, MathsUtils.eThrowTypes ThrowType, Rigidbody rb, out float time, out Vector3 Velocity)
    {
        Debug.Log("Trying to throw");

        if (MathsUtils.CalculateTrajectory(Pos, Target, Speed, ThrowType, out Velocity,
                out time))
        {
            Debug.Log("Throw Will hit");
            rb.AddForce(Velocity,ForceMode.VelocityChange);
        }
        else
        {
            Debug.Log("Throw Will not hit");
        }
    }

    public IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
