using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gnome : Enemy
{
    public LineRenderer lm;
    public LineRenderer lm2;
    public Transform startPoint;
    public Transform endPoint;
    public float moveDuration;
    public Poolee smallerSelfPrefab;
    public bool saller;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        b.Damageable.Death += OnDeath;
    }

    // Update is called once per frame
    public override void Update()
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

    public void OnDeath(GameObject o)
    {
        if(saller)
        {
            StartCoroutine(SpawnGnomes());
        }
        
       
    }

    public IEnumerator SpawnGnomes()
    {
        bool foundValidPosition;
        NavMeshHit hit;

        while (!NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            yield return null;
        }

        for (int i = 0; i < 4; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * 1f;
            Vector3 randomPoint = hit.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas);
            GameObject g = PoolManager.instance.SpawnFromPool(smallerSelfPrefab, hit.position, Quaternion.identity);
            Debug.Log(g.GetInstanceID());
        }



    }
}
