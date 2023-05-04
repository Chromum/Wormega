using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;
    public GunPart part;
    public PlayerManager playerManager;

    public void AcceptItem()
    {
        if (item != null)
        {
            PlayerInventory pi = GameObject.FindObjectOfType<PlayerInventory>();
            pi.AddNewItem(item);
            Cursor.lockState = CursorLockMode.None;
            item = null;
            gameObject.SetActive(false);
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
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);
        }

        try
        {
            playerManager.currentRoom.rm.boxes[0].gameObject.SetActive(false);
            playerManager.currentRoom.rm.boxes[1].gameObject.SetActive(false);
        }
        catch{}

        item = null;
        part = null;
        GameManager.instance.TogglePause();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void DeclineItem()
    {
        item = null;
        part = null;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.instance.TogglePause();
        gameObject.SetActive(false);
     

    }
}
