using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRoomManager : MonoBehaviour
{

    public int aliveEnemys;
    public MapSpriteSelector mapSpriteSelector;
    public List<Poolee> EnemyPrefabs;
    public AudioSource Source;
    public AudioClip clip;
    public GameObject HeliPrefab;
    public List<EnemyT> t = new List<EnemyT>();
    public BossAI ai;
    public GameObject intro;
    public void Start()
    {
        mapSpriteSelector.PER += PlayerEnter;
    }

    public void SpawnEnemies(int count)
    {
        GameObject j = GameObject.Instantiate(HeliPrefab, new Vector3(transform.position.x - 50f, transform.position.y + 60f, transform.position.z), Quaternion.identity);
        StartCoroutine(FlyAwayEnum(j.GetComponent<AudioSource>(), j));
    }

    public void PlayerEnter()
    {

        intro.SetActive(true);
        GameManager.instance.Wave1.transform.parent.gameObject.SetActive(true);
    }

    public IEnumerator FlyAwayEnum(AudioSource SFX, GameObject g)
    {

        yield return new WaitForSeconds(5f);
        int e = ai.currentWave.enemysToSpawn;
        aliveEnemys = e;
        for (int i = 0; i < e; i++)
        {
            SpawnEnemy(g);
            SFX.PlayOneShot(clip);
            yield return new WaitForSeconds(.75f);
        }
        g.GetComponent<Animator>().SetTrigger("FlyAway");
    }
    public IEnumerator LerpObject(NavMeshAgent start, float duration, Vector3 POS)
    {
        Vector3 startPosition = POS;

        Vector3 randomPosition;
        NavMeshHit hit;

        randomPosition = Random.insideUnitSphere * 10f + transform.position;
        while (!NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
        {
            randomPosition = Random.insideUnitSphere * 10f + transform.position;
            yield return null;
        }


        float startTime = Time.time;
        start.transform.position = startPosition;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            start.transform.position = Vector3.Lerp(startPosition, hit.position, t);
            yield return null;
        }

        start.transform.position = hit.position;
        start.Warp(hit.position);
        start.enabled = true;
        start.GetComponent<Enemy>().enabled = true;
    }

    public Vector3 ValidPosition(Vector3 POS, Vector3 Origin)
    {

        bool isValid = false;
        Vector3 randomPosition = POS;
        Vector3 returner;
        while (isValid)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
            {
                RaycastHit h;
                if (Physics.Raycast(Origin, (Origin - hit.position), out h))
                {
                    if (h.collider.gameObject.layer != 8)
                    {
                        return hit.position;
                    }



                }
            }
            else
                randomPosition = Random.insideUnitSphere * 10f + transform.position;
        }

        return Vector3.zero;

    }

    public void SpawnEnemy(GameObject Heli)
    {
        // Calculate the total spawn chance
        float totalSpawnChance = 0f;
        foreach (EnemySpawnData spawnData in ai.currentWave.enemySpawnArray)
        {
            totalSpawnChance += spawnData.chanceOfSpawning;
        }

        // Generate a random value between 0 and the total spawn chance
        float randomValue = Random.Range(0f, totalSpawnChance);

        // Iterate through the spawn array to find the enemy to spawn
        foreach (EnemySpawnData spawnData in ai.currentWave.enemySpawnArray)
        {
            // If the random value is within the current spawn chance range, spawn the corresponding enemy
            if (randomValue <= spawnData.chanceOfSpawning)
            {
                SpawnEnemyOfType(spawnData.enemyType,Heli);
                break;
            }

            // Subtract the current spawn chance from the random value
            randomValue -= spawnData.chanceOfSpawning;
        }
    }

    private void SpawnEnemyOfType(Poolee enemyType,GameObject Heli)
    {
        try
        {
            NavMeshAgent g = PoolManager.instance.SpawnFromPool(enemyType, Heli.transform.GetChild(14).position, Quaternion.identity).GetComponent<NavMeshAgent>();
            g.enabled = false;
            g.GetComponent<Enemy>().enabled = false;
            t.Add(new EnemyT { EnemyOBJ = g.gameObject, Alive = true });
            g.GetComponent<Damageable>().Death += EnemyDied;
            StartCoroutine(LerpObject(g, 3f, Heli.transform.GetChild(14).position));
        }
        catch
        {

        }

        Debug.Log("Spawning enemy: " + enemyType);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    public void EnemyDied(GameObject g)
    {
        aliveEnemys--;
        Destroy(g);
        if (aliveEnemys <= 0)
        {
            BringBossBack();
        }
    }

    public void BringBossBack()
    {
        ai.gameObject.SetActive(true);
        ai.BackIntoRing();
    }


}
