using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooleeManager : MonoBehaviour
{
    public Poolee poolType;
    public UnityEvent pooleeDeathEvent;

    public void ReturnToPool()
    {
        PoolManager.instance.ReturnToPool(poolType,this.gameObject);
    }
}
