using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Enemy
{
    public LineRenderer lm;
    public LineRenderer lm2;
    public Transform startPoint;
    public Transform endPoint;
    public float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button]
    public void TempAttackDebug()
    {
        Attack.Execute(this);
    }

    public void ShootFX()
    {
        lm.enabled = true;
        lm.positionCount = 2;
        lm.SetPosition(0, startPoint.transform.position);
        StartCoroutine(MoveLine());
    }


    public IEnumerator MoveLine()
    {
        float moveStartTime = Time.time;

        while (Time.time - moveStartTime < moveDuration)
        {
            // Calculate the current progress
            float progress = (Time.time - moveStartTime) / moveDuration;

            // Interpolate the position based on the progress
            Vector3 newPosition = Vector3.Lerp(startPoint.position, endPoint.position, progress);

            // Update the LineRenderer position
            lm.SetPosition(1, newPosition);

            yield return null;
        }

        // Set the final position to ensure accuracy
        lm.SetPosition(1, endPoint.position);
    }
}
