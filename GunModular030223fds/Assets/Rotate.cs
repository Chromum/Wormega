using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    public RectTransform t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       t.Rotate(new Vector3(t.eulerAngles.x,speed*Time.deltaTime,t.eulerAngles.z),Space.Self); 
    }
}
