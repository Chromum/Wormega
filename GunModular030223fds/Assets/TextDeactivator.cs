using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDeactivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Wave1HealthBar").GetComponent<HealthBar>().transform.parent.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
