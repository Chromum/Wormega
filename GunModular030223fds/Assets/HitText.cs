using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = System.Random;


public class HitText : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    public float moveSpeed;
    public Poolee poolee;
    public Countdown disspearCountdown;
    private Color startColor;
    public Color currentColor;

    public void Awake()
    {
        startColor = TextMeshPro.color;
        currentColor = startColor;
    }

    public void OnEnable()
    {
        currentColor = startColor;
        disspearCountdown.StartCountdown();
        TextMeshPro.color = startColor;
        
    }

    public void Update()
    {
        transform.position += new Vector3(UnityEngine.Random.RandomRange(-.3f,.3f), moveSpeed) * Time.deltaTime;
        disspearCountdown.CountdownUpdate();
        currentColor.a -= 3f * Time.deltaTime;
        TextMeshPro.color = currentColor;

        transform.LookAt(Camera.main.transform);

        
        if(disspearCountdown.HasFinished())
            PoolManager.instance.ReturnToPool(poolee,gameObject);
        
    }
}
