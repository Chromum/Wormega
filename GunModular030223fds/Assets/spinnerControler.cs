using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerControler : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform Startr;
    public Transform End;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, Startr.transform.position);
        lineRenderer.SetPosition(1, End.transform.position);

    }
}
