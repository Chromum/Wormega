using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public bool isMeleeing;
    public float BaseDamage;
    public float Damage;
    public Animator animator;
    public Transform tar;
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
        RaycastHit raycastHit;
        if (Physics.Raycast(tar.transform.position, tar.transform.forward, out raycastHit, 3f))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Melee");
                raycastHit.transform.GetComponent<Damageable>().DoDamage(Damage, raycastHit.transform.position);
            }
        }
    }

    public void Reset()
    {
        isMeleeing = false;
    }

}
