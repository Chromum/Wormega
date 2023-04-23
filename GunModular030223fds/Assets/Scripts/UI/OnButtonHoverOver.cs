using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHoverOver : MonoBehaviour,IPointerEnterHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
