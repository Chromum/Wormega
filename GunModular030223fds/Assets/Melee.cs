using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public bool isMeleeing;
    public float BaseDamage;
    public float Damage;
    public Animator animator;
    public Transform test;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        Damage = BaseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (!isMeleeing)
        {
            isMeleeing = true;
            Invoke(nameof(Reset), 1f);
            animator.SetTrigger("Whip");
        }
    }
    public void DoDamge()
    {
        Debug.Log("Attempting to do damage");
        RaycastHit raycastHit;
        Collider[] c = Physics.OverlapBox(Camera.main.transform.position, new Vector3(3f, 3f, 3f));
        foreach (var VARIABLE in c)
        {
            if (VARIABLE.CompareTag("Enemy")) 
            {
                Debug.Log("Melee");
                VARIABLE.transform.GetComponent<Damageable>().DoDamage(Damage, VARIABLE.transform.position);
            }
        }
    }

    public void Reset()
    {
        isMeleeing = false;
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(Camera.main.transform.position, new Vector3(5,5,5));
    }
}
