using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpinnerCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }
    
    public IEnumerator SpinnerCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
