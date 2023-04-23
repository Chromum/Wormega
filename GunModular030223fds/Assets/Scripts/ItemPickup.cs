using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item Item;

    public Light Glow;
    public Color Common;
    public Color Rare;
    public Color Legendary;

    public void Start()
    {
        switch (Item.Rareity)
        {
            case Rareity.Common:
                Glow.color = Common;
                break;
            case Rareity.Rare:
                Glow.color = Rare;
                break;
            case Rareity.Legendary:
                Glow.color = Legendary;
                break;
            default:
                break;
        }
    }

    public override void Interact(GameObject g)
    {
        base.Interact(g);
        PickUpItem();
    }

    public void PickUpItem()
    {
        PlayerStatsManager PSM = GameObject.FindObjectOfType<PlayerStatsManager>();
        PSM.AddStats(Item.ItemStats);
        Destroy(this.gameObject);
    }
}
