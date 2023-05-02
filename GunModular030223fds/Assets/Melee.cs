using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public bool isMeleeing;
    public float BaseDamage;
    public float Damage;
    public Animator animator;
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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, 2.5f))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                raycastHit.transform.GetComponent<Damageable>().DoDamage(Damage, raycastHit.transform.position);
            }
        }
    }

    public void Reset()
    {
        isMeleeing = false;
    }

}
