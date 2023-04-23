using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAblityActivator : MonoBehaviour
{
    public CigaretteManager abilityManager;

    public bool Check;

    public Countdown Countdown;
    // Start is called before the first frame update
    void Start()
    {
        abilityManager.abOver += SC;
    }

    public void SC()
    {
        Countdown.StartCountdown();
    }
    
    // Update is called once per frame
    void Update()
    {


        Countdown.CountdownUpdate();

    }
}