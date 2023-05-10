using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    
    public List<Poolee> poolConfigurations;

    private Dictionary<Poolee, Queue<GameObject>> pools;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pools = new Dictionary<Poolee, Queue<GameObject>>();

        foreach (Poolee config in poolConfigurations)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < config.Amount; i++)
            {
                GameObject obj = Instantiate(config.poolPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            pools.Add(config, objectPool);
        }
    }
    public GameObject SpawnFromPool(Poolee poolee, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(poolee))
        {
            Debug.LogWarning("Pool with Poolee " + poolee.name + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = HandlePoolStrategy(poolee);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        Debug.Log("A");
        return objectToSpawn;
    }

    public void ReturnToPool(Poolee poolee, GameObject obj)
    {
        obj.SetActive(false);
        pools[poolee].Enqueue(obj);
    }


    private GameObject HandlePoolStrategy(Poolee poolType)
    {
        Queue<GameObject> pool = pools[poolType];

        GameObject obj;
        switch (poolType.poolType)
        {
            case PoolType.Grow:
                obj = Instantiate(poolConfigurations.Find(c => c.poolType == poolType.poolType).poolPrefab);
                break;
            case PoolType.ReuseOldest:
                obj = pool.Dequeue();
                pool.Enqueue(obj);
                break;
            case PoolType.ReuseNewest:
                obj = pool.Peek();
                break;
            case PoolType.ReuseNone:
                obj = null;
                break;
            case PoolType.ReuseAndOrGrow:
                if (pool.Count > 0)
                {
                    obj = pool.Dequeue();
                }
                else
                {
                    obj = Instantiate(poolConfigurations.Find(c => c.poolType == poolType.poolType).poolPrefab);
                }
                break;
            default:
                obj = null;
                break;
        }

        return obj;
    }
}


public enum PoolType
{ 	
    Grow,
    ReuseOldest,
    ReuseNewest,
    ReuseNone,
    ReuseAndOrGrow, 
}
