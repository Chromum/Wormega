using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        mapSpriteSelector.PER += PlayerEnter;
        mapSpriteSelector.PEER += PlayerExits;
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
    public IEnumerator LerpObject(NavMeshAgent start, Vector3 end, float duration, Vector3 POS)
    {
        Vector3 startPosition = POS;
        float startTime = Time.time;
        start.transform.position = startPosition;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            start.transform.position = Vector3.Lerp(startPosition, end, t);
            yield return null;
        }

        // Ensure we end exactly at the target position
        start.transform.position = end;
        start.Warp(end);
        start.enabled = true;
        start.GetComponent<Enemy>().enabled = true;
    }

    public void SpawnEnemy(GameObject Heli)
    {
        NavMeshAgent g = GameObject.Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)]).GetComponent<NavMeshAgent>();
        g.enabled = false;
        g.GetComponent<Enemy>().enabled = false;
        g.transform.position = Heli.transform.GetChild(14).position;
        NavMeshHit h;
        NavMesh.SamplePosition(g.transform.position, out h, 100f, NavMesh.AllAreas);
        StartCoroutine(LerpObject(g, h.position, 3f, Heli.transform.GetChild(14).position));
    }
}
