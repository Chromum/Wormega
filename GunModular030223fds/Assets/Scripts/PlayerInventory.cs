using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<Item,StoredItem> Items = new Dictionary<Item,StoredItem>();
    public PlayerStatsManager statsManager;
    public Gun Gun;

    [Header("UI")]
    public GameObject SpriteListOne;
    public List<GameObject> SprtieListOneItems = new List<GameObject>();
    public GameObject SpriteListTwo;
    public List<GameObject> SprtieListTwoItems = new List<GameObject>();
    public GameObject SpriteListThree;
    public List<GameObject> SprtieListThreeItems = new List<GameObject>();

    public GameObject BlankSprite;
    public void AddNewItem(Item i) 
    {
        if(Items.ContainsKey(i))
        {
            Items[i].Quantity += 1;
            Debug.Log(i.Name);

            foreach(GameObject g in SprtieListOneItems)
            {
                Debug.Log(g.name);
                if (g.name == i.Name)
                {
                    g.transform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + Items[i].Quantity;
                }
            }
            foreach (GameObject g in SprtieListTwoItems)
            {
                if (g.name == i.Name)
                    g.transform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + Items[i].Quantity;
            }
            foreach (GameObject g in SprtieListThreeItems)
            {
                if (g.name == i.Name)
                    g.transform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + Items[i].Quantity;
            }
 
        }
        else
        {
            Items.Add(i, new StoredItem { Item = i, Quantity = 1 });
            if (SprtieListOneItems.Count >= 8)
            {
                if (SprtieListTwoItems.Count >= 8)
                {
                    if (SprtieListThreeItems.Count >= 8)
                    {

                    }
                    else
                    {
                        Image image = Instantiate(BlankSprite, SpriteListThree.transform).GetComponent<Image>();
                        image.sprite = i.Image;
                        image.GetComponentInChildren<TextMeshProUGUI>().text = "x1";
                        image.gameObject.name = i.Name;
                        SprtieListThreeItems.Add(image.gameObject);

                    }
                }
                else
                {
                    Image image = Instantiate(BlankSprite, SpriteListTwo.transform).GetComponent<Image>();
                    image.sprite = i.Image;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = "x1";
                    image.gameObject.name = i.Name;
                    SprtieListTwoItems.Add(image.gameObject);


                }
            }
            else
            {
                Debug.Log("DId that");
                Image image = Instantiate(BlankSprite, SpriteListOne.transform).GetComponent<Image>();
                image.sprite = i.Image;
                image.GetComponentInChildren<TextMeshProUGUI>().text = "x1";
                image.gameObject.name = i.Name;
                SprtieListOneItems.Add(image.gameObject);

            }
        }
        statsManager.AddStats(i.ItemStats);
    }
    public void RemoveItem(Item i) 
    {
        if (Items.ContainsKey(i))
        {
            Items[i].Quantity -= 1;
        }
        statsManager.RemoveStats(i.ItemStats);
    }


    
}
[System.Serializable]
public class StoredItem 
{
    public Item Item;
    public int Quantity = 1;
}


