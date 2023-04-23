using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PartIcon : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Part Part;
    private PlayerInventory Inv;
    public Image Sr;
    public Sprite Sprite;
    public Sprite DefualtSprite;
    public bool isHovering;
    public bool hasHovered;
    public GameObject StatsPannel;
    public BoxCollider2D col;

    public TextMeshProUGUI Damage, FireRate, MagSize, Accuracy;
    void Start()
    {
        Inv = GameObject.FindObjectOfType<PlayerInventory>();
        SetSprite();
        Physics2D.queriesHitTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpriteNeedsChanging())
            SetSprite();

        if (isHovering && !hasHovered)
        {
            // Run method on hover over
            OnHoverOver();
            hasHovered = true;
        }
        else if (!isHovering && hasHovered)
        {
            // Run method when stopping hovering over
            OnHoverExit();
            hasHovered = false;
        }
    }

    public bool SpriteNeedsChanging()
    {
        Sprite s = null;
        switch (Part)
        {
            case Part.Barrel:
                s = Inv.Gun.Barrel.Sprite;
                break;
            case Part.Module:
                s = Inv.Gun.Module.Sprite;
                break;
            case Part.Grip:
                s = Inv.Gun.Grip.Sprite;
                break;
            case Part.Mag:
                s = Inv.Gun.Magazine.Sprite;
                break;
            case Part.Sight:
                s = Inv.Gun.Sight.Sprite;
                break;
        }
        if (s != Sprite)
            return true;
        else return false;
    }
    
    public void SetSprite()
    {
        switch (Part)
        {
            case Part.Barrel:
                if (Inv.Gun.Barrel.Sprite != null)
                    Sprite = Inv.Gun.Barrel.Sprite;
                else
                    Sprite = DefualtSprite;
                break;
            case Part.Module:
                if (Inv.Gun.Module.Sprite != null)
                    Sprite = Inv.Gun.Module.Sprite;
                else
                    Sprite = DefualtSprite;
                break;
            case Part.Grip:
                if (Inv.Gun.Grip.Sprite != null)
                    Sprite = Inv.Gun.Grip.Sprite;
                else
                    Sprite = DefualtSprite;
                break;
            case Part.Mag:
                if (Inv.Gun.Magazine.Sprite != null)
                    Sprite = Inv.Gun.Magazine.Sprite;
                else
                    Sprite = DefualtSprite;
                break;
            case Part.Sight:
                if (Inv.Gun.Sight.Sprite != null)
                    Sprite = Inv.Gun.Sight.Sprite;
                else
                    Sprite = DefualtSprite;
                break;
        }

        Sr.sprite = Sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void OnHoverOver()
    {
        Sr.gameObject.SetActive(false);
        StatsPannel.SetActive(true);
    }

    private void OnHoverExit()
    {
        Sr.gameObject.SetActive(true);
        StatsPannel.SetActive(false);
    }
    
    
}

public enum Part { Barrel, Sight, Mag, Module, Grip }