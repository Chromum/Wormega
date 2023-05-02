using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemBox : Interactable
{
    public bool Gun;
    public bool Upgrade;
    public GunPart GunPart;
    public Item Item;
    public List<GlowStruct> glowObjects = new List<GlowStruct>();
    public List<GlowStruct> hologramObjects = new List<GlowStruct>();

    public RareityItem Common, Rare, Legendary, Mythic;
    public Database Database;
    public Transform Items;
    public float rotationSpeed = 10.0f;
    public AudioSource Au;
    public AudioClip Keypad, Ding, DePreasure;
    public bool TurnedOn = false;

    public void Start()
    {
        if(TurnedOn)
        {
            StartCoroutine(BoxStart());
            StartCoroutine(Spin());
        }
    }

    public void Update()
    {
        if(TurnedOn)
            if (isOpen == false)
                Items.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }

    public bool isOpen;

    public IEnumerator BoxStart()
    {
        if (TurnedOn)
        {
            while (!isOpen)
            {
                SetRandomColour();
                yield return new WaitForSeconds(.5f);
            }
        }
 
    }

    public IEnumerator Spin()
    {
        if(TurnedOn)
        {
            while (!isOpen)
            {
                if (Gun)
                {
                    int i = Random.Range(0, Items.GetChild(0).childCount);
                    foreach (Transform child in Items.GetChild(0).transform)
                    {
                        child.gameObject.SetActive(false);
                        if (child == Items.GetChild(0).GetChild(i))
                            child.gameObject.SetActive(true);
                    }
                }
                if (Upgrade)
                {
                    int i = Random.Range(0, Items.GetChild(1).childCount);
                    foreach (Transform child in Items.GetChild(1).transform)
                    {
                        child.gameObject.SetActive(false);
                        if (child == Items.GetChild(1).GetChild(i))
                            child.gameObject.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(.5f);
            }
        }
       
    }

    [Button]
    public void OpenBox()
    {
        isOpen = true;
        PickItem();
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        StartCoroutine(Wait());
        

        
    }

    public IEnumerator Wait()
    {
        

        yield return new WaitForSeconds(3f);
        GameObject g = GameObject.FindObjectOfType<PlayerManager>().ItemPickupUI;
        g.SetActive(true);
        g.transform.GetChild(4).gameObject.SetActive(false);
        g.transform.GetChild(5).gameObject.SetActive(false);


        if (Gun)
        {
            g.transform.GetChild(5).gameObject.SetActive(true   );
            g.GetComponent<ItemHolder>().part = GunPart;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GunPart.Name;
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GunPart.Bio;
            g.transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Damage: " + GunPart.Damage.ToString();
            g.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Fire Rate: " + GunPart.FireRate.ToString();
            g.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Mag Size: " + GunPart.MagSize.ToString();
            g.transform.GetChild(5).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Accuracy: " + GunPart.Accuracy.ToString();
            if (GunPart.GetType() == typeof(Barrel))
            {
                Barrel b = (Barrel)GunPart;
                g.transform.GetChild(5).GetChild(4).gameObject.SetActive(true);
                g.transform.GetChild(5).GetChild(5).gameObject.SetActive(true);
                g.transform.GetChild(5).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Shot Mode : " + b.ShotMode.ToString();
                g.transform.GetChild(5).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Shot Mode : " + b.barrelType.ToString();
            }
            else
            {
                g.transform.GetChild(5).GetChild(4).gameObject.SetActive(false);
                g.transform.GetChild(5).GetChild(5).gameObject.SetActive(false);
            }
        }
        if (Upgrade)
        {
            g.transform.GetChild(4).gameObject.SetActive(true);
            g.GetComponent<ItemHolder>().item = Item;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Item.Name;
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Item.Description;
            g.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health: " + Item.ItemStats.Health.ToString();
            g.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Speed: " + Item.ItemStats.Speed.ToString();
            g.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Jump Height: " + Item.ItemStats.JumpHeight.ToString();
            g.transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Ability Cooldown: " + Item.ItemStats.AbilityCooldown.ToString();
            g.transform.GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Strength: " + Item.ItemStats.Strength.ToString();
            g.transform.GetChild(4).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Stamina: " + Item.ItemStats.Stamina.ToString();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void Interact(GameObject GO)
    {
        if(TurnedOn && !isOpen )
        {
            base.Interact(GO);
            OpenBox();
        }
    }


    public void SetType(Rareity rareity)
    {
        Material matGlow = null;
        Material matHolo = null;
        switch (rareity)
        {
            case Rareity.Common:
                matGlow = Common.glowMaterial;
                matHolo = Common.HoloMaterial;
                break;
            case Rareity.Rare:
                matGlow = Rare.glowMaterial;
                matHolo = Rare.HoloMaterial;
                break;
            case Rareity.Legendary:
                matGlow = Legendary.glowMaterial;
                matHolo = Legendary.HoloMaterial;
                break;
            case Rareity.Mythic:
                matGlow = Mythic.glowMaterial;
                matHolo = Mythic.HoloMaterial;
                break;
            default:
                break;
        }

        foreach (GlowStruct glow in glowObjects)
        {
            Material[] m = glow.meshRenderer.sharedMaterials;
            m[glow.Value] = matGlow;
            glow.meshRenderer.sharedMaterials = m;
        }
        foreach (GlowStruct glow in hologramObjects)
        {
            Material[] m = glow.meshRenderer.sharedMaterials;
            m[glow.Value] = matHolo;
            glow.meshRenderer.sharedMaterials = m;
        }
    }

    [Button]
    public void SetRandomColour()
    {
        Rareity r = (Rareity)System.Enum.ToObject(typeof(Rareity), Random.Range(0, 4));
        SetType(r);
    }

    public void KeypadSound()
    {
        AudioUtils.PlaySoundWithPitch(Au, Keypad, Random.Range(1, 3));
    }
    public void DingSound()
    {
        AudioUtils.PlaySoundWithPitch(Au, Ding, 1f);
    }
    public void DePreasureSound()
    {
        AudioUtils.PlaySoundWithPitch(Au, DePreasure, 1f);

    }





    [Header("Debug")]
    public bool Debug;
    [Button]
    public void SetGlowObjects()
    {
        glowObjects.Clear();

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            var t = child.GetComponent<MeshRenderer>();
            if (t != null)
            {
                for (int i = 0; i < t.sharedMaterials.Length; i++)
                {
                    if (t.sharedMaterials[i].name.Contains("Glow"))
                    {
                        glowObjects.Add(new GlowStruct
                        {
                            meshRenderer = t,
                            Value = i
                        });
                    }


                }
            }
        }



    }
    [Button]
    public void SetHologramObjects()
    {
        hologramObjects.Clear();
        Transform[] allChildren = Items.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            MeshRenderer mr = child.GetComponent<MeshRenderer>();

            if (mr != null)
            {
                for (int i = 0; i < mr.sharedMaterials.Length; i++)
                {
                    hologramObjects.Add(new GlowStruct
                    {
                        meshRenderer = mr,
                        Value = i
                    });
                }
            }

        }
    }
    [Button]
    public void PickItem()
    {
        float f = Random.Range(0f, 1f);
        Rareity rareity;
        if (f < .5f)
            rareity = Rareity.Common;
        else if (f < .85f)
            rareity = Rareity.Rare;
        else if (f < .95)
            rareity = Rareity.Legendary;
        else
            rareity = Rareity.Mythic;

        if (Gun)
        {
            GunPart Part = null;
            switch (rareity)
            {
                case Rareity.Common:
                    Part = Database.Common[Random.Range(0, Database.Common.Count)];
                    break;
                case Rareity.Rare:
                    Part = Database.Rare[Random.Range(0, Database.Rare.Count)];
                    break;
                case Rareity.Legendary:
                    Part = Database.Legendary[Random.Range(0, Database.Legendary.Count)];
                    break;
                case Rareity.Mythic:
                    Part = Database.Mythic[Random.Range(0, Database.Mythic.Count)];
                    break;
            }
            GunPart = Part;
            SetItem(Part.objectName, Part.Rareity);
        }
        if (Upgrade)
        {
            Item Part = null;
            switch (rareity)
            {
                case Rareity.Common:
                    Part = Database.CommonUp[Random.Range(0, Database.CommonUp.Count)];
                    break;
                case Rareity.Rare:
                    Part = Database.RareUp[Random.Range(0, Database.RareUp.Count)];
                    break;
                case Rareity.Legendary:
                    Part = Database.LegendaryUp[Random.Range(0, Database.LegendaryUp.Count)];
                    break;
                case Rareity.Mythic:
                    Part = Database.MythicUp[Random.Range(0, Database.MythicUp.Count)];
                    break;
            }
            Item = Part;
            SetItem(Part.objectName, Part.Rareity);
        }
    }

    public void SetItem(string objectName,Rareity R)
    {
        Transform t = null;
        UnityEngine.Debug.Log(objectName);
        if (Gun)
            t = Items.GetChild(0);
        if (Upgrade)
            t = Items.GetChild(1);

        foreach (Transform child in t)
        {
            child.gameObject.SetActive(false);
            if (objectName.ToLower() == child.name.ToLower())
                child.gameObject.SetActive(true);
            if (objectName == "" && child.name == "cube")
                child.gameObject.SetActive(true);
        }
        
        SetType(R);
    }
    [Button]
    public void TurnBoxOn()
    {
        TurnedOn = true;
        StartCoroutine(BoxStart());
        StartCoroutine(Spin());
    }

}
    [System.Serializable]
public struct GlowStruct
{
    public MeshRenderer meshRenderer;
    public int Value;
}
