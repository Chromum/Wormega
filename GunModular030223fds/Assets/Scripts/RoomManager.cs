using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class RoomManager : MonoBehaviour
{
    public MapSpriteSelector mapSpriteSelector;

    private bool FirstTime;
    private bool InRoom;

    public GameObject HeliPrefab;

    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public AudioClip SF;

    public int Waves;

    public List<EnemyT> t = new List<EnemyT>();
    // Start is called before the first frame update
    void Start()
    {
        mapSpriteSelector.PER += PlayerEnter;
        mapSpriteSelector.PEER += PlayerExits;
        Waves = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerEnter()
    {
        if (FirstTime == false)
            FirstTimeEnter();
        else
            InRoom = true;

    }

    public void PlayerExits()
    {

    }
    public void FirstTimeEnter()
    {
        FirstTime = true;
        GameObject j = GameObject.Instantiate(HeliPrefab, new Vector3(transform.position.x - 50f, transform.position.y + 60f, transform.position.z), Quaternion.identity);
        StartCoroutine(FlyAwayEnum(j.GetComponent<AudioSource>(), j));
        
    }

    


    public IEnumerator FlyAwayEnum(AudioSource SFX, GameObject g)
    {
        yield return new WaitForSeconds(5f);
        int e = Random.Range(4, 7);
        for (int i = 0; i < e; i++)
        {
            Debug.Log("HHAHH");
            SpawnEnemy(g);
            SFX.PlayOneShot(SF);
            yield return new WaitForSeconds(1f);
        }
        g.GetComponent<Animator>().SetTrigger("FlyAway");

    }
    public IEnumerator LerpObject(NavMeshAgent start, float duration, Vector3 POS)
    {
        Vector3 startPosition = POS;

        Vector3 randomPosition = POS;
        NavMeshHit hit;


        while (!NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
        {
            randomPosition = Random.insideUnitSphere * 10f + transform.position;
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
        while(isValid)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 5f, NavMesh.AllAreas))
            {
                RaycastHit h;
                if (Physics.Raycast(Origin,(Origin - hit.position),out h))
                {
                    if(h.collider.gameObject.layer != 8)
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
        NavMeshAgent g = GameObject.Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)]).GetComponent<NavMeshAgent>();
        g.enabled = false;
        g.GetComponent<Enemy>().enabled = false;
        g.transform.position = Heli.transform.GetChild(14).position;
        StartCoroutine(LerpObject(g, 3f, Heli.transform.GetChild(14).position));
    }
}

[System.Serializable]
public struct EnemyT
{
    GameObject EnemyOBJ;
    bool Alive;
}
