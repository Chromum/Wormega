using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackHole : MonoBehaviour
{
    public float force;
    public float suckDuration;
    public float Radius;
    
    [Button]
    public void Suck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);
        List<Collider> enemies = new List<Collider>();
        foreach (var VARIABLE in colliders)
        {
            if(VARIABLE.tag == "Enemy")
                enemies.Add(VARIABLE);
                
        }
        
        StartCoroutine(SuckEnemies(enemies));
    }
    
    public IEnumerator SuckEnemies(List<Collider> enemies)
    {
        // Calculate initial distances and speeds for each enemy
        float[] distances = new float[enemies.Count];
        float[] speeds = new float[enemies.Count];

        for (int i = 0; i < enemies.Count; i++)
        {
            distances[i] = Vector3.Distance(enemies[i].transform.position, transform.position);
            speeds[i] = distances[i] / suckDuration;
        }

        float startTime = Time.time;
        while (Time.time - startTime < suckDuration)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].transform.position = Vector3.MoveTowards(
                    enemies[i].transform.position, 
                    transform.position, 
                    speeds[i] * Time.deltaTime
                );
            }

            yield return null; // Wait until the next frame
        }

        // Ensure all enemies have reached the target position
        foreach (Collider enemy in enemies)
        {
            enemy.transform.position = transform.position;
        }
    }
    

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere (transform.position, Radius);

    }
}
