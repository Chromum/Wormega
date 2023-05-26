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
        base.Update();   
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
            
            Vector3 startPosition = hit.position;

            Vector3 randomPosition;
            NavMeshHit hit2;

            randomPosition = Random.insideUnitSphere * 1f + transform.position;
            while (!NavMesh.SamplePosition(randomPosition, out hit2, 5f, NavMesh.AllAreas))
            {
                randomPosition = Random.insideUnitSphere * 1f + transform.position;
                yield return null;
            }
            
            
            Vector2 randomCircle = Random.insideUnitCircle * 1f;
            Vector3 randomPoint = hit.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            GameObject g = PoolManager.instance.SpawnFromPool(smallerSelfPrefab, hit2.position, Quaternion.identity);
            StartCoroutine(PlaceAgentOnNavMesh(g.GetComponent<NavMeshAgent>()));
            Debug.Log(g.GetInstanceID());
        }


        
        

    }
    
    private System.Collections.IEnumerator PlaceAgentOnNavMesh(NavMeshAgent agent)
    {
        float radius = 2f; // Maximum distance to search for a valid position
        int maxAttempts = 10; // Maximum number of attempts to find a valid position

        int attempts = 0;
        bool positionFound = false;

        while (!positionFound && attempts < maxAttempts)
        {
            Vector3 randomOffset = Random.insideUnitSphere * radius;
            Vector3 randomPosition = transform.position + randomOffset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, radius, NavMesh.AllAreas))
            {
                agent.enabled = false;
                agent.transform.position = hit.position;
                agent.enabled = true;
                positionFound = true;
            }

            attempts++;
            yield return null;
        }

        if (!positionFound)
        {
            Debug.LogWarning("Failed to find a valid position on the NavMesh for agent placement.");
        }
    }
}
