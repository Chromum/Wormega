using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteractor : MonoBehaviour
{
    public GameObject Player;
    public KeyCode InteractKey;
    public float Range;

    public GameObject InteractText;
    public LayerMask LayerMask;

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit i;
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out i, Range,LayerMask))
        {
            if (i.collider.tag == "Interactable")
            {
                InteractText.SetActive(true);
                if (Input.GetKeyDown(InteractKey))
                {
                    i.collider.GetComponent<Interactable>().Interact(Player);
                }
            }
            
        }
        else
            InteractText.SetActive(false);
    }
}
