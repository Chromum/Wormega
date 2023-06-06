using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LevelPassThroughManager : MonoBehaviour
{

    public Button SB, MB, LB;
    public TextMeshProUGUI i,i2;
    public bool Gnomes;
    public void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public int RoomCount;

    public void Small()
    {
        RoomCount = 8;
        i.text = "8 Rooms";
    }
    
    public void Medium()
    {
        RoomCount = 15;
        i.text = "15 Rooms";

    }
    
    public void Large()
    {
        RoomCount = 25;
        i.text = "25 Rooms";

    }

    public void ToggleGnomes()
    {
        Gnomes = !Gnomes;
        if (Gnomes)
            i2.text = "Gnomes: On";
        else
            i2.text = "Gnomes: Off";
    }



}
