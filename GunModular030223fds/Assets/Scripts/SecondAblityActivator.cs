using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondAblityActivator : MonoBehaviour
{
    public CigaretteManager abilityManager;

    public bool Check;

    public Countdown Countdown;

    public Slider slid;

    private bool startChecking = false;
    // Start is called before the first frame update
    void Start()
    {
        abilityManager.abOver += SC;
    }

    public void SC()
    {
        Countdown.StartCountdown();
        startChecking = true;
    }
    
    // Update is called once per frame
    void Update()
    {


        Countdown.CountdownUpdate();

        if (startChecking)
        {
            if (Countdown.HasFinished() == false)
            {
                float i = 1 - (Countdown.value / Countdown.Count);

                slid.value = i;
            }
            else
            {
                slid.value = 1;
                startChecking = false;
            }
        }
        
        
        
    }
}