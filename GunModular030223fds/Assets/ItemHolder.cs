using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;
    public GunPart part;


    public void AcceptItem()
    {
        if (item != null)
        {
            PlayerInventory pi = GameObject.FindObjectOfType<PlayerInventory>();
            pi.AddNewItem(item);
        }
        else
        {
            Gun PlayerGun = GameObject.FindObjectOfType<PlayerInventory>().transform.GetComponentInChildren<Gun>();

            switch (part.Type)
            {
                case Typwe.Grip:
                    PlayerGun.Grip = part;
                    break;
                case Typwe.Barrel:
                    PlayerGun.Barrel = (Barrel)part;
                    break;
                case Typwe.Sight:
                    PlayerGun.Sight = part;
                    break;
                case Typwe.Mag:
                    PlayerGun.Magazine = part;
                    break;
                case Typwe.Module:
                    PlayerGun.Module = (Module)part;
                    break;
                default:
                    break;
            }
            PlayerGun.RecalculateStats();
        }
    }
    public void DeclineItem()
    {
        item = null;
        part = null;
        GameManager.instance.TogglePause();
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(false);
    }
}
